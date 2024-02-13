using AddressBook.Api.Data.Entities;
using Bogus;

namespace AddressBook.Api.Data;

public class BookEntryFaker : Faker<BookEntry>
{
	public BookEntryFaker(AddressFaker addrFaker)
	{
 
    UseSeed(1969) // Use any number
      .RuleFor(c => c.Id, f => ++f.IndexVariable)
      .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
      .RuleFor(c => c.FirstName, f => f.Name.FirstName())
      .RuleFor(c => c.MiddleName, f => f.Name.FirstName())
      .RuleFor(c => c.LastName, f => f.Name.LastName())
      .RuleFor(c => c.CellPhone, f => f.Phone.PhoneNumberFormat().OrNull(f, 0.5f))
      .RuleFor(c => c.HomePhone, f => f.Phone.PhoneNumberFormat().OrNull(f, 0.5f))
      .RuleFor(c => c.WorkPhone, f => f.Phone.PhoneNumberFormat().OrNull(f, 0.5f))
      .RuleFor(c => c.Gender, f => f.Person.Gender.ToString())
      .RuleFor(c => c.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
      .RuleFor(c => c.DateOfBirth, f => f.Person.DateOfBirth);
  }
}
