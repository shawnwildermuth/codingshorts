using FluentValidation;

namespace AddressBook.Api.Validators;

public class BookEntryValidator : AbstractValidator<BookEntryModel>
{
  public BookEntryValidator()
  {
    RuleFor(b => b.FirstName).NotEmpty();
    RuleFor(b => b.LastName).NotEmpty();
    RuleFor(b => b.DateOfBirth)
      .HasValidBirthday()
      //.Must(IsValidBirthdate)
      //.Custom((b, ctx) =>
      //{
      //  if (b.Year <= 1900)
      //  {
      //    ctx.AddFailure("The {PropertyName} has a year that is too old.");
      //  }
      //  else if (b.Year >= DateTime.Today.Year)
      //  {
      //    ctx.AddFailure("The {PropertyName} must be less than the current date.");
      //  }
      //})
      //.WithMessage("Bad birthday")
      ;
  }

  private bool IsValidBirthdate(DateTime birthday)
  {
    return birthday.Year > 1900 && birthday.Year < DateTime.Today.Year;
  }
}
