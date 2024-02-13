using FluentValidation;
using FluentValidation.Validators;

namespace AddressBook.Api.Validators;

public class BirthdayPropertyValidator<T> : PropertyValidator<T, DateTime>
{
  public override string Name => "BirthdayValidator";

  public override bool IsValid(ValidationContext<T> ctx, DateTime b)
  {
    if (b.Year <= 1900)
    {
      ctx.AddFailure("The {PropertyName} has a year that is too old.");
    }
    else if (b.Year >= DateTime.Today.Year)
    {
      ctx.AddFailure("The {PropertyName} must be less than the current date.");
    }
    else
    {
      return true;
    }

    return false;
  }
}

public static class BirthdayPropertyValidatorExtensions
{
  public static IRuleBuilderOptions<T, DateTime> HasValidBirthday<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
  {
    return ruleBuilder.SetValidator(new BirthdayPropertyValidator<T>());
  }
}
