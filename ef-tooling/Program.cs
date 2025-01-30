using MinimalApis.Discovery;
using NewEfFeatures.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FlightContext>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapApis();

app.Run();
