using System.Reflection;
using AddressBook.Api.Meters;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using dotenv.net;
using FluentValidation;
using OpenTelemetry.Metrics;
using WilderMinds.MinimalApiDiscovery;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookContext>();
builder.Services.AddTransient<BookEntryFaker>();
builder.Services.AddTransient<AddressFaker>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddMapsterMaps();

builder.Services.AddScoped<EntryMeter>();

builder.Services.AddOpenTelemetry()
  .UseAzureMonitor(cfg =>
  {
    cfg.ConnectionString = builder.Configuration["APP_INSIGHTS_CONNECTION"];
  })
  .WithMetrics(cfg =>
  {
    cfg.AddMeter(EntryMeter.MeterName)
       .AddAspNetCoreInstrumentation();
       //.AddConsoleExporter();
  });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapApis();

app.MapFallbackToFile("/index.html");

app.Run();

