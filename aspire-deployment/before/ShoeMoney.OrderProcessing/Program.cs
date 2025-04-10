using Microsoft.EntityFrameworkCore;

using ShoeMoney;
using ShoeMoney.Data;
using ShoeMoney.OrderProcessing;

var builder = Host.CreateApplicationBuilder(args);

builder.EnableMonitoring();

builder.EnableMessaging(cfg => cfg.AddConsumer<OrderConsumer>());

builder.Services.AddDbContext<ShoeContext>(opt =>
{
  opt.UseSqlServer(
    $"{builder.Configuration.GetConnectionString("show-money")};MultipleActiveResultSets=true"
    );
});

builder.AddRabbitMQClient("order-queue");

var host = builder.Build();

host.Run();
