using AddressBook.Api.Data.Entities;
using Bogus;

namespace AddressBook.Api.Data;

public class AddressFaker : Faker<Address>
{
  public AddressFaker()
  {
    UseSeed(1969)
      .RuleFor(c => c.Id, f => ++f.IndexVariable)
      .RuleFor(c => c.Name, f => f.Random.Words(2))
      .RuleFor(c => c.Line1, f => f.Address.StreetAddress())
      .RuleFor(c => c.Line2, f => f.Address.SecondaryAddress().OrNull(f, .5f))
      .RuleFor(c => c.Line3, f => f.Address.SecondaryAddress().OrNull(f, .05f))
      .RuleFor(c => c.CityTown, f => f.Address.City())
      .RuleFor(c => c.StateProvince, f => f.Address.State())
      .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
      .RuleFor(c => c.Country, f => f.Address.Country());
  }
}