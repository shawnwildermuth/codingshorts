using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MassTransit;

using ShoeMoney.Data;

namespace ShoeMoney.OrderProcessing.Consumers;
public class OrderConsumer(IServiceProvider provider, ILogger<OrderConsumer> logger) 
  : IConsumer<OrderCreated>
{
  public async Task Consume(ConsumeContext<OrderCreated> context)
  {
    using var scope = provider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ShoeContext>();
    dbContext.Add(context.Message.Order);
    if (await dbContext.SaveChangesAsync() > 0)
    {
      await context.Publish(new OrderSubmitted(context.Message.Order));
      logger.LogInformation("Saved new order");
    }
  }
}
