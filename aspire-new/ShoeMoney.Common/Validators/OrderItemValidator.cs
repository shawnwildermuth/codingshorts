using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using ShoeMoney.Data.Entities;

namespace ShoeMoney.Validators;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
  public OrderItemValidator()
  {
    RuleFor(i => i.ProductId).NotEmpty();
    RuleFor(i => i.Quantity).NotEmpty();
    RuleFor(i => i.Price).NotEmpty();
  }
}
