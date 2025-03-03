using System.Text;
using System.Text.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using ShoeMoney;
using ShoeMoney.Data;
using ShoeMoney.Data.Entities;
using ShoeMoney.Models;

namespace ShoeMoney.OrderProcessing;

public class OrderProcessingWorker(
  ILogger<OrderProcessingWorker> logger,
  IConnection connection,
  IServiceProvider services)
  : IHostedService, IDisposable
{
  IModel? _channel;

  public Task StartAsync(CancellationToken stoppingToken)
  {

    _channel = connection.CreateModel();

    _channel.QueueDeclare(ShoeConstants.OrderQueueName, false, false, false);
    _channel.QueueDeclare(ShoeConstants.ErrorQueueName, false, false, false);

    stoppingToken.Register(() => Stop());

    var consumer = new EventingBasicConsumer(_channel);
    consumer.Received += ProcessQueue;

    _channel.BasicConsume(consumer,
      ShoeConstants.OrderQueueName);

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Dispose();
    return Task.CompletedTask;
  }

  async void ProcessQueue(object? sender, BasicDeliverEventArgs args)
  {
    try
    {
      var json = Encoding.UTF32.GetString(args.Body.Span);
      var model = JsonSerializer.Deserialize<Order>(json);

      if (model is not null)
      {
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
          SendError("Could not process Order");
        }
      }

    }
    catch (Exception ex)
    {
      logger.LogError($"Error during Order process: {ex}");
      SendError("Error during Order process", ex);
    }

  }

  void SendError(string error, Exception? ex = null)
  {
    var message = new ErrorMessage()
    {
      Message = error,
      ExceptionMessage = ex?.Message
    };
    var json = JsonSerializer.Serialize(message);
    var body = Encoding.UTF8.GetBytes(json);
    _channel.BasicPublish("", ShoeConstants.ErrorQueueName, null, body);
    logger.LogInformation("Error Reported");
  }

  public void Dispose()
  {
    Stop();
  }


  void Stop()
  {
    _channel?.Dispose();
  }

}

