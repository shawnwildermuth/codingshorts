using VitePwa;
using VitePwa.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<FakeCustomers>();

var app = builder.Build();

app.UseFileServer();
app.MapCustomerApis();

app.Logger.LogInformation($"Environment: {app.Environment.EnvironmentName}");


app.Run();
