using Bogus;

namespace VitePwa.Data;

public class FakeCustomers : Faker<Customer>
{
	public FakeCustomers()
	{
		UseSeed(32768);
		RuleFor(c => c.Id, o => o.IndexFaker);
    RuleFor(c => c.CompanyName, o => o.Company.CompanyName());
    RuleFor(c => c.Contact, o => o.Name.FullName());
    RuleFor(c => c.Phone, o => o.Phone.PhoneNumberFormat());
    RuleFor(c => c.Address1, o => o.Address.StreetAddress());
    RuleFor(c => c.Address2, o => o.Address.SecondaryAddress().OrNull(o, .7f));
    RuleFor(c => c.Address3, o => o.Address.BuildingNumber().OrNull(o,.9f));
    RuleFor(c => c.City, o => o.Address.City());
    RuleFor(c => c.State, o => o.Address.StateAbbr());
    RuleFor(c => c.PostalCode, o => o.Address.ZipCode());
    RuleFor(c => c.CreditLimit, o => o.Random.Int(1000, 30000));
  }
}