using Microsoft.EntityFrameworkCore;
using MinimalApis.Discovery;
using ShoeMoney.Data;
using ShoeMoney.Data.Seeding;
using FluentValidation;
using ShoeMoney.Validators;
using System.Text.Json.Serialization;
using ShoeMoney;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.EnableMonitoring();

builder.AddRedisDistributedCache("Cache");
builder.AddRabbitMQClient("OrderQueue");
builder.Services.AddDbContext<ShoeContext>(opt =>
{
  opt.UseSqlServer(
    $"{builder.Configuration.GetConnectionString("ShoeMoney")};MultipleActiveResultSets=true",
    options => options.EnableRetryOnFailure(5, TimeSpan.FromMinutes(1), null));
});

builder.Services.AddTransient<Seeder>();

builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHealthChecks();

builder.Services.AddCors(cfg => cfg.AddDefaultPolicy(bldr =>
{
  bldr.AllowAnyHeader();
  bldr.AllowAnyOrigin();
  bldr.AllowAnyMethod();
}));

builder.EnableMessaging();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
  try {
  var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
  using var scope = scopeFactory.CreateScope();

  var ctx = scope.ServiceProvider.GetRequiredService<ShoeContext>();
  ctx.Database.EnsureCreated();

  // Enqueue the seeding
  var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
  seeder.Seed();
  }
  catch (Exception ex)
  {
    app.Logger.LogError(ex, "Failed to seed");
  }
}

app.UseCors();

app.MapHealthChecks("/health");

app.MapApis();

app.Run();
