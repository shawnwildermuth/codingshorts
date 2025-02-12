using Microsoft.EntityFrameworkCore;
using SqlDev.Data;
using SqlDev.StoreFakers;
using System.Text.Json.Serialization;
using static Microsoft.AspNetCore.Http.Results;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Fakers>();

builder.Services.AddDbContext<StoreContext>(cfg =>
{
  //cfg.UseSqlServer(builder.Configuration.GetConnectionString("StoreDb"));
  cfg.UseNpgsql(builder.Configuration.GetConnectionString("StoreDb-Postgres"));
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
  options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.MapGet("/api/customers", async (StoreContext ctx) =>
{

  var results = await ctx.Customers
    .Include(c => c.Address)
    .Include(c => c.Orders)
    .OrderBy(c => c.CompanyName)
    .ToListAsync();

  return Ok(results);
});

app.MapGet("/api/customers/{id:int}", async (StoreContext ctx, int id) =>
{

  var result = await ctx.Customers
    .Include(c => c.Address)
    .Include(c => c.Orders)
    .OrderBy(c => c.CompanyName)
    .Where(c => c.Id == id)
    .FirstOrDefaultAsync();

  if (result is null) return NotFound();

  return Ok(result);
});

app.Run();
