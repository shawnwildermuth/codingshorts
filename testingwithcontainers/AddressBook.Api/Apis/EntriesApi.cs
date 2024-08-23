using System.Net;
using AddressBook.Api.Validators;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace AddressBook.Apis;

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

    return Ok(results);
  }

  public static async Task<IResult> GetEntries(IBookRepository repository)
  {
    var results = await repository.GetAllEntries();

    return Ok(results.Adapt<IEnumerable<BookEntryModel>>());
  }

  public static async Task<IResult> GetEntry(IBookRepository repository, int id)
  {
    var result = await repository.GetEntry(id);

    if (result is null) return NotFound();

    return Ok(result.Adapt<BookEntryModel>());
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
        return Created($"/api/book/entries/{entry.Id}", entry.Adapt<BookEntryModel>());
      }
    }

    return Problem("Failed to create new book entry");
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
        return Ok(entry.Adapt<BookEntryModel>());
      }
      else
      {
        return Problem("No changes made");
      }
    }

    return NotFound();
  }

  public static async Task<IResult> DeleteEntry(IBookRepository repository, int id)
  {
    var entry = await repository.GetEntry(id);
    if (entry is not null)
    {
      repository.Remove(entry);
      if (await repository.SaveAllAsync())
      {
        return Ok();
      }
      else
      {
        return Problem("Failed to delete entry.");
      }
    }

    return NotFound();
  }


}
