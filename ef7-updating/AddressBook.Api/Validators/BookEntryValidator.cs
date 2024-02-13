using FluentValidation;

namespace AddressBook.Api.Validators;

public class BookEntryValidator : AbstractValidator<BookEntryModel>
{
  public BookEntryValidator()
  {
    RuleFor(b => b.FirstName).NotEmpty();
    RuleFor(b => b.LastName).NotEmpty();

  }
}
