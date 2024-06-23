using Microsoft.EntityFrameworkCore;
using SqlDev.Data;
using SqlDev.StoreFakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Fakers>();

builder.Services.AddDbContext<StoreContext>(cfg => {
  cfg.UseSqlServer(builder.Configuration.GetConnectionString("StoreDb"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
