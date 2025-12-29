using Microsoft.EntityFrameworkCore;
using MinimalApis.Discovery;
using ShoeMoney.Data;
using ShoeMoney.Data.Seeding;
using FluentValidation;
using ShoeMoney.Validators;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShoeContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoeMoneyDb"));
});

builder.AddTelemetry();

builder.Services.AddTransient<Seeder>();

builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
  var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
  using var scope = scopeFactory.CreateScope();

  var ctx = scope.ServiceProvider.GetRequiredService<ShoeContext>();
  ctx.Database.EnsureCreated();

  // Enqueue the seeding
  var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
  seeder.Seed();
}

app.MapDefaultEndpoints();

app.MapApis();

app.Run();
