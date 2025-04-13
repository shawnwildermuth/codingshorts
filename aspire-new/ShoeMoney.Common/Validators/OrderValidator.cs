using FluentValidation;

using ShoeMoney.Data.Entities;

namespace ShoeMoney.Validators;

public class OrderValidator : AbstractValidator<Order>
{
  public OrderValidator(AddressValidator addressValidator, OrderItemValidator orderItemValidator)
  {
    RuleFor(o => o.OrderDate).NotEmpty();
    RuleForEach(o => o.Items).NotEmpty().SetValidator(orderItemValidator);
    RuleFor(o => o.ShippingAddress).Custom((addr, ctx) =>
    {
      if (addr != null)
      {
        var result = addressValidator.Validate(addr);
        if (!result.IsValid)
        {
          foreach (var failure in result.Errors) ctx.AddFailure(failure);
        }
      }
    });

  }
}
