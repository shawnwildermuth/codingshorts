using System.Net;
using AddressBook.Api.Validators;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MinimalApis.Discovery;


namespace AddressBook.Api.Apis;

public class EntriesApi : IApi
{
  private const string LOOKUPS_KEY = "LOOKUPS";

  public void Register(IEndpointRouteBuilder builder)
  {
    var grp = builder.MapGroup("/api/entries");

    grp.MapGet("lookup", GetLookup);

    grp.MapGet("", GetEntries);

    grp.MapGet("{id:int}", GetEntry);

    grp.MapPost("", PostEntry)
      .Validate<BookEntryModel>();

    grp.MapPut("{id:int}", PutEntry)
      .Validate<BookEntryModel>();

    grp.MapDelete("{id:int}", DeleteEntry);
  }

  public async static Task<IResult> GetLookup(IBookRepository repository, IMemoryCache cache)
  {

    IEnumerable<BookEntry>? lookup = cache.Get<IEnumerable<BookEntry>>(LOOKUPS_KEY);

    if (lookup is null)
    {
      lookup = await repository.GetLookupEntries();
      cache.Set(LOOKUPS_KEY, lookup);
    }


    var results = lookup.Adapt<IEnumerable<BookEntryLookupModel>>();

    return Results.Ok(results);
  }

  public static async Task<IResult> GetEntries(IBookRepository repository, IMemoryCache cache)
  {

    IEnumerable<BookEntry>? results = cache.Get<IEnumerable<BookEntry>>("ENTRIES");

    if (results is null)
    {
      results = await repository.GetAllEntries();
      cache.Set("ENTRIES", results);
    }

    return Results.Ok(results.Adapt<IEnumerable<BookEntryModel>>());
  }

  public static async Task<IResult> GetEntry(IBookRepository repository, int id)
  {
    var result = await repository.GetEntry(id);

    if (result is null) return Results.NotFound();

    return Results.Ok(result.Adapt<BookEntryModel>());
  }

  public static async Task<IResult> PostEntry(IBookRepository repository, BookEntryModel model, IMemoryCache cache)
  {
    // Ignores attached addresses
    var entry = model.Adapt<BookEntry>();
    if (entry is not null)
    {
      repository.Add(entry);

      if (await repository.SaveAllAsync())
      {
        cache.Remove(LOOKUPS_KEY);
        return Results.Created($"/api/book/entries/{entry.Id}", entry.Adapt<BookEntryModel>());
      }
    }

    return Results.Problem("Failed to create new book entry");
  }

  public static async Task<IResult> PutEntry(IBookRepository repository, int id, BookEntryModel model, IMemoryCache cache)
  {
    var entry = await repository.GetEntry(id);

    if (entry is not null && entry.Id == id)
    {
      // Ignores attached addresses
      model.Adapt(entry);

      if (await repository.SaveAllAsync())
      {
        cache.Remove(LOOKUPS_KEY);
        return Results.Ok(entry.Adapt<BookEntryModel>());
      }
      else
      {
        return Results.Problem("No changes made");
      }
    }

    return Results.NotFound();
  }

  public static async Task<IResult> DeleteEntry(IBookRepository repository, int id, IMemoryCache cache)
  {
    var entry = await repository.GetEntry(id);
    if (entry is not null)
    {
      repository.Remove(entry);
      if (await repository.SaveAllAsync())
      {
        cache.Remove(LOOKUPS_KEY);
        return Results.Ok();
      }
      else
      {
        return Results.Problem("Failed to delete entry.");
      }
    }

    return Results.NotFound();
  }


}
