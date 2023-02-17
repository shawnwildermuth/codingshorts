using DemoAPI;
using DemoAPI.Data;
using DemoAPI.Models;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.AddSingleton<BechdelRepository>();

var app = builder.Build();

var group = app.MapGroup("/api/films");

group.MapGet("", async (BechdelRepository repo) =>
{
  return Results.Ok(await repo.GetAll());
})
  .Produces(200);

group.MapGet("{id:regex(tt[0-9]*)}", async (BechdelRepository repo, string id) =>
{
  Console.WriteLine(id);
  var film = await repo.GetOne(id);
  if (film is null) return Results.NotFound("Couldn't find Film");
  return Results.Ok(film);
})
  .Produces(200);

group.MapGet("{year:int}", async (BechdelRepository repo, int year, bool? passed) =>
{
  var results = await repo.GetByYear(year, passed);
  if (results.Count() == 0)
  {
    return Results.NoContent();
  }

  return Results.Ok(results);
})
  .Produces(200);

group.MapPost("", (BechdelRepository repo, Film model) =>
{
  return Results.Created($"/api/films/{model.IMDBId}", model);
})
  .Produces(201);

app.Run();

