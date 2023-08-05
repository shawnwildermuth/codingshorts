using Dealership.Data;
using FluentValidation;

namespace Dealership.Validators;

public class VehicleValidator : AbstractValidator<Vehicle>
{
  public VehicleValidator()
  {
    RuleFor(v => v.Make)
      .NotEmpty();

    RuleFor(v => v.Model)
      .NotEmpty();

    RuleFor(v => v.Vin)
      .Length(17);

  }
}
