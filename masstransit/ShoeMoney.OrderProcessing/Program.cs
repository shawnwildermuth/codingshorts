using System.Reflection;

using MassTransit;

using Microsoft.EntityFrameworkCore;

using ShoeMoney.Data;
using ShoeMoney.OrderProcessing;

var builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddHostedService<OrderProcessingWorker>();

builder.Services.AddDbContext<ShoeContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoeMoneyDb"));
});

//builder.AddRabbitMQClient("orderQueue");

builder.Services.AddMassTransit(cfg =>
{

  cfg.AddConsumers(Assembly.GetExecutingAssembly());

  var connectionString = builder.Configuration.GetConnectionString("orderQueue");

  cfg.UsingRabbitMq((ctx, rab) =>
  {
    rab.Host(new Uri(connectionString!));
    rab.ConfigureEndpoints(ctx);
  });
});

var host = builder.Build();
host.Run();
