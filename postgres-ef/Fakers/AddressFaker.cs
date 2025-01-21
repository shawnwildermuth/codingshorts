using Bogus;
using SqlDev.Data.Entities;

namespace SqlDev.StoreFakers;

public class AddressFaker : Faker<Address>
{
  public AddressFaker()
  {
    var id = 0;
    UseSeed(1969)
      .RuleFor(c => c.Id, f => ++id)
      .RuleFor(c => c.Address1, f => f.Address.StreetAddress())
      .RuleFor(c => c.Address2, f => f.Address.SecondaryAddress().OrNull(f, .5f))
      .RuleFor(c => c.City, f => f.Address.City())
      .RuleFor(c => c.StateProvince, f => f.Address.State())
      .RuleFor(c => c.PostalCode, f => f.Address.ZipCode());
  }
}