using DemoAPI;
using DemoAPI.Apis;
using DemoAPI.Data;
using DemoAPI.Models;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApis();

// Register Services
builder.Services.AddSingleton<BechdelRepository>();

var app = builder.Build();

var apis = app.Services.GetServices<IApi>();

app.MapApis();

app.Run();

