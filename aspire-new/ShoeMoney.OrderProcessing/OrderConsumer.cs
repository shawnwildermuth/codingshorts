using System.Diagnostics;

using MassTransit;

using ShoeMoney.Data;
using ShoeMoney.Models;

namespace ShoeMoney.OrderProcessing;
public class OrderConsumer(ILogger<OrderConsumer> logger,
  IServiceProvider services) : IConsumer<OrderCreated>
{
  public async Task Consume(ConsumeContext<OrderCreated> consumer)
  {
    try
    {
      var consumerActivity = new ActivitySource("OrdersApi");

      using var activity = consumerActivity.StartActivity("Saving order");

      var model = consumer.Message.Order;

      logger.LogInformation("Retrieved Order from Queue");

      // Remove the products so we don't try to insert them
      foreach (var item in model.Items)
      {
        if (item.Product is not null) item.Product = null;
      }

      using var scope = services.CreateScope();
      var context = scope.ServiceProvider.GetRequiredService<ShoeContext>();

      context.Add(model);

      if (await context.SaveChangesAsync() == 0)
      {
        logger.LogError($"No changes when adding order");
        await consumer.Publish(new OrderError(model, "Could not process Order"));
      }
      else
      {
        logger.LogInformation($"Order Saved");
        await consumer.Publish(new OrderSaved(model));
      }

    }
    catch (Exception ex)
    {
      logger.LogError($"Error during Order Save process: {ex}");
      await consumer.Publish(new OrderError(consumer.Message.Order, $"Could not process Order. Exception thrown: {ex}"));
    }


  }
}
