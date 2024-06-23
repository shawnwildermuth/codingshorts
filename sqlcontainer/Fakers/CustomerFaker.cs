using Bogus;
using SqlDev.Data.Entities;

namespace SqlDev.StoreFakers;

public class CustomerFaker : Faker<Customer>
{
  AddressFaker _addrFaker = new AddressFaker();

	public CustomerFaker()
  {
    var id = 1;

	  UseSeed(1969) // Use any number
    .RuleFor(c => c.Id, _ => id++)
    .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
    .RuleFor(c => c.ContactName, f => f.Name.FullName())
    .RuleFor(c => c.Phone, f => f.Phone.PhoneNumberFormat())
    .RuleFor(c => c.Address, _ => _addrFaker.Generate(1)
                                            .First()
                                            .OrNull(_, .1f));
  }
}
