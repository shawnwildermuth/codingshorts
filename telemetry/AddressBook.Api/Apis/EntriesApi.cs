using System.Net;
using AddressBook.Api.Validators;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;

namespace AddressBook.Api.Apis;

public class EntriesApi : IApi
{
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

  public async static Task<IResult> GetLookup(IBookRepository repository)
  {
    var lookup = await repository.GetLookupEntries();

    var results = lookup.Adapt<IEnumerable<BookEntryLookupModel>>();

    return Results.Ok(results);
  }

  public static async Task<IResult> GetEntries(IBookRepository repository)
  {
    var results = await repository.GetAllEntries();

    return Results.Ok(results.Adapt<IEnumerable<BookEntryModel>>());
  }

  public static async Task<IResult> GetEntry(IBookRepository repository, int id)
  {
    var result = await repository.GetEntry(id);

    if (result is null) return Results.NotFound();

    return Results.Ok(result.Adapt<BookEntryModel>());
  }

  public static async Task<IResult> PostEntry(IBookRepository repository, BookEntryModel model)
  {
    // Ignores attached addresses
    var entry = model.Adapt<BookEntry>();
    if (entry is not null)
    {
      repository.Add(entry);

      if (await repository.SaveAllAsync())
      {
        return Results.Created($"/api/book/entries/{entry.Id}", entry.Adapt<BookEntryModel>());
      }
    }

    return Results.Problem("Failed to create new book entry");
  }

  public static async Task<IResult> PutEntry(IBookRepository repository, int id, BookEntryModel model)
  {
    var entry = await repository.GetEntry(id);

    if (entry is not null && entry.Id == id)
    {
      // Ignores attached addresses
      model.Adapt(entry);

      if (await repository.SaveAllAsync())
      {
        return Results.Ok(entry.Adapt<BookEntryModel>());
      }
      else
      {
        return Results.Problem("No changes made");
      }
    }

    return Results.NotFound();
  }

  public static async Task<IResult> DeleteEntry(IBookRepository repository, int id)
  {
    var entry = await repository.GetEntry(id);
    if (entry is not null)
    {
      repository.Remove(entry);
      if (await repository.SaveAllAsync())
      {
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
