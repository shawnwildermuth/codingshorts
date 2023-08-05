using Bogus;
using MappingTest.Entities;

namespace MappingTest.Fakers;

public class CustomerFaker : Faker<Customer>
{
  public CustomerFaker(bool deepFakes = true)
  {
    var addressFaker = new AddressFaker();
    var orderFaker = new OrderFaker();

    UseSeed(8080);
    RuleFor(c => c.Id, f => f.IndexFaker);
    RuleFor(c => c.CompanyName, f => f.Company.CompanyName());
    RuleFor(c => c.ContactName, f => f.Name.FullName());
    RuleFor(c => c.Phone, f => f.Phone.PhoneNumber());

    if (deepFakes)
    {
      RuleFor(c => c.Address, f => addressFaker.Generate().OrNull(f, .1f));
      RuleFor(c => c.Orders, (f,c) => orderFaker.WithContext(nameof(Customer.Id), f.IndexFaker).GenerateBetween(0, 3));
    }
  }
}

