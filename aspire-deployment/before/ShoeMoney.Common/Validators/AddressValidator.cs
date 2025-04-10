using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using ShoeMoney.Data.Entities;

namespace ShoeMoney.Validators;

public class AddressValidator : AbstractValidator<Address>
{
  public AddressValidator()
  {
    RuleFor(a => a.Line1).NotEmpty();
    RuleFor(a => a.CityTown).NotEmpty();
    RuleFor(a => a.StateProvince).NotEmpty();
    RuleFor(a => a.PostalCode).NotEmpty();
    RuleFor(a => a.ShippingPhoneNumber).NotEmpty();
  }
}
