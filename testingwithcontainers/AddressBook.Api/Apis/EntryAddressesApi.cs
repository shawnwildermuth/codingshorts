using AddressBook.Api.Validators;
using Mapster;
using WilderMinds.MinimalApiDiscovery;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace AddressBook.Apis;

public class EntryAddressesApi : IApi
{
  public void Register(IEndpointRouteBuilder builder)
  {
    var grp = builder.MapGroup("/api/entries/{id:int}/addresses");

    grp.MapGet("", GetAddresses);
    grp.MapGet("{addressId:int}", GetAddress);

    grp.MapPost("", PostAddress)
      .Validate<AddressModel>();

    grp.MapPut("{addressId:int}", PutAddress)
      .Validate<AddressModel>();

    grp.MapDelete("{addressId:int}", DeleteAddress);
  }

  public static async Task<IResult> GetAddresses(IBookRepository repository, int id)
  {
    var results = await repository.GetAddressesForUser(id);

    return Ok(results.Adapt<IEnumerable<AddressModel>>());
  }

  public static async Task<IResult> GetAddress(IBookRepository repository, int id, int addressId)
  {
    var results = await repository.GetAddressForUser(id, addressId);

    return Ok(results.Adapt<AddressModel>());
  }

  public async Task<IResult> PostAddress(IBookRepository repository, int id, AddressModel model)
  {
    var address = model.Adapt<Address>();

    // Update the book to attach of it.
    address.BookEntryId = id;
    
    if (address is not null)
    {
      repository.Add(address);

      if (await repository.SaveAllAsync())
      {
        return Created($"/api/book/entries/{id}/addresses/{address.Id}", address.Adapt<AddressModel>());
      }
    }

    return Problem("Failed to create new book entry");

  }

  public static async Task<IResult> PutAddress(IBookRepository repository, int id, int addressId, AddressModel model)
  {
    if (model.Id == default) return Problem("Must include ID in the body of the request");

    var address = await repository.GetAddressForUser(id, addressId);

    if (address is not null && model.Id == addressId)
    {
      // Ignores attached addresses
      model.Adapt(address);

      if (await repository.SaveAllAsync())
      {
        return Ok(address.Adapt<AddressModel>());
      }
    }

    return NotFound();
  }

  public static async Task<IResult> DeleteAddress(IBookRepository repository, int id, int addressId)
  {
    var address = await repository.GetAddressForUser(id, addressId);

    if (address is not null)
    {
      repository.Remove(address);
      if (await repository.SaveAllAsync())
      {
        return Ok();
      }
      else
      {
        return Problem("Failed to delete address.");
      }
    }

    return NotFound();
  }
}
