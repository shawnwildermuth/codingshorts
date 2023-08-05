using Bogus;
using MappingTest.Entities;

namespace MappingTest.Fakers;

public class OrderFaker : Faker<Order>
{
  public OrderFaker()
  {
    var itemFaker = new OrderItemFaker();

    UseSeed(8080);
    RuleFor(o => o.Id, f => f.IndexFaker);
    RuleFor(o => o.OrderDate, f => f.Date.Between(new DateTime(2000, 01, 31).ToUniversalTime(), DateTime.UtcNow));
    RuleFor(o => o.Terms, f => f.PickRandomParam("Net 30", "Net 15", "Due On Receipt", "Net 90"));
    RuleFor(o => o.OrderItems, f => itemFaker.GenerateBetween(1,15));
    RuleFor(o => o.CustomerId, f => f.FromContext<int>(nameof(Customer.Id)));
  }

}