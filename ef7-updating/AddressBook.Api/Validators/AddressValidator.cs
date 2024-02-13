using FluentValidation;

namespace AddressBook.Api.Validators;

public class AddressValidator : AbstractValidator<AddressModel>
{
  public AddressValidator()
  {
    RuleFor(a => a.Line1).NotEmpty();
    RuleFor(a => a.CityTown).NotEmpty();
    RuleFor(a => a.StateProvince).NotEmpty();
    RuleFor(a => a.Name).NotEmpty();
    RuleFor(a => a.PostalCode).NotEmpty();

  }
}
