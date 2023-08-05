using Bogus;
using MappingTest.Entities;

namespace MappingTest.Fakers;

public class AddressFaker : Faker<Address>
{
  public AddressFaker()
  {
    UseSeed(8080);
    RuleFor(a => a.Id, f => f.IndexFaker);
    RuleFor(a => a.Address1, f => f.Address.StreetAddress());
    RuleFor(a => a.Address2, f => f.Address.SecondaryAddress().OrNull(f, .85f));
    RuleFor(a => a.City, f => f.Address.City());
    RuleFor(a => a.StateProvince, f => f.Address.State());
    RuleFor(a => a.PostalCode, f => f.Address.ZipCode());  
  }
}