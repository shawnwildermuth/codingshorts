using System.Text.RegularExpressions;
using FluentValidation;
using MinimalApiValidation.Data;

public class PersonValidator : AbstractValidator<Person>
{
  public PersonValidator()
  {
    RuleFor(p => p.Id).GreaterThan(0);
    RuleFor(p => p.FullName).MaximumLength(255).NotEmpty();
    RuleFor(p => p.Email).EmailAddress();
    RuleFor(p => p.Phone)
    .NotEmpty()
    .Must(phone => Regex.IsMatch(phone!, @"^\+?(?:[0-9] ?){6,14}[0-9]$"))
    .WithMessage("Must be a valid phone number");
  }
}