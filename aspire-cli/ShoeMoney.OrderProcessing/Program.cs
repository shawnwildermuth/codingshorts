using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using ShoeMoney.Data;
using ShoeMoney.OrderProcessing;

var builder = WebApplication.CreateBuilder(args);

builder.AddTelemetry();

builder.Services.AddHostedService<OrderProcessingWorker>();

builder.Services.AddDbContext<ShoeContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoeMoneyDb"));
});

var host = builder.Build();

host.MapDefaultEndpoints();

host.Run();
