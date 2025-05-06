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
    $"{builder.Configuration.GetConnectionString("shoe-money")};MultipleActiveResultSets=true",
    options => options.EnableRetryOnFailure(5, TimeSpan.FromMinutes(1), null));
});

var host = builder.Build();

host.Run();
