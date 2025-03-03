using Microsoft.EntityFrameworkCore;
using MinimalApis.Discovery;
using ShoeMoney.Data;
using ShoeMoney.Data.Seeding;
using FluentValidation;
using ShoeMoney.Validators;
using System.Text.Json.Serialization;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddRabbitMQClient("orderQueue");
builder.Services.AddDbContext<ShoeContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoeMoneyDb"));
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

builder.Services.AddMassTransit(cfg =>
{
  var connectionString = builder.Configuration.GetConnectionString("orderQueue");

  cfg.UsingRabbitMq((ctx, rab) =>
  {
    rab.Host(new Uri(connectionString!));
    rab.ConfigureEndpoints(ctx);
  });
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

app.UseCors();

app.MapHealthChecks("/health");

app.MapApis();

app.Run();
