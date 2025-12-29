using System.Diagnostics;
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
  IServiceProvider services)
  : IHostedService, IDisposable
{
  IChannel? _channel;
  IConnection? _connection;

    
  public async Task StartAsync(CancellationToken stoppingToken)
  {
    if (_connection is null)
    {
      var factory = new ConnectionFactory()
      {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
      };

      _connection = await factory.CreateConnectionAsync();
    }

    _channel = await _connection.CreateChannelAsync();

    await _channel.QueueDeclareAsync(ShoeConstants.OrderQueueName, false, false, false);
    await _channel.QueueDeclareAsync(ShoeConstants.ErrorQueueName, false, false, false);

    stoppingToken.Register(() => Stop());

    var consumer = new AsyncEventingBasicConsumer(_channel);
    consumer.ReceivedAsync += ProcessQueueAsync;

    await _channel.BasicConsumeAsync(ShoeConstants.OrderQueueName, true, "", consumer);
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Dispose();
    return Task.CompletedTask;
  }

  async Task ProcessQueueAsync(object? sender, BasicDeliverEventArgs args)
  {
    try
    {
      Activity.Current?.AddTag("Parent_Span_ID", Activity.Current.ParentSpanId.ToString());

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
          await SendError("Could not process Order");
        }
      }

    }
    catch (Exception ex)
    {
      logger.LogError($"Error during Order process: {ex}");
      await SendError("Error during Order process", ex);
    }

  }

  async Task SendError(string error, Exception? ex = null)
  {
    var message = new ErrorMessage()
    {
      Message = error,
      ExceptionMessage = ex?.Message
    };
    var json = JsonSerializer.Serialize(message);
    var body = Encoding.UTF8.GetBytes(json);
    await _channel!.BasicPublishAsync("", ShoeConstants.ErrorQueueName, true, body);
    logger.LogInformation("Error Reported");
  }

  public void Dispose()
  {
    Stop();
  }


  void Stop()
  {
    _channel?.Dispose();
    _connection?.Dispose();
  }

}

