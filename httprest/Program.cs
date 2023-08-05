using Dealership.Apis;
using Dealership.Data;
using Dealership.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Repo>();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<VehicleValidator>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapApis();

app.Run();
