using Bogus;
using MappingTest.Entities;

namespace MappingTest.Fakers;

public class OrderItemFaker : Faker<OrderItem>
{
  public OrderItemFaker()
  {
    UseSeed(8080);
    RuleFor(i => i.Id, f => f.IndexFaker);
    RuleFor(i => i.Quantity, f => f.Random.Int(1,255));
    RuleFor(i => i.Discount, f => f.PickRandomParam(0, .1, .25));
    RuleFor(i => i.UnitPrice, f => f.Finance.Amount(.1m, 100m));
    RuleFor(i => i.Description, f => f.Commerce.Product());
    RuleFor(i => i.Unit, f => f.PickRandomParam("Each", "Box", "Bag", "lbs."));
    RuleFor(i => i.Description, f => f.Commerce.Product());
  }
}