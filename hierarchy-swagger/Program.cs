using DemoAPI;
using DemoAPI.Data;
using DemoAPI.Models;
using SwaggerHierarchySupport;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.AddSingleton<BechdelRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var group = app.MapGroup("/api/films");

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(opt => opt.AddHierarchySupport());
}

group.MapGet("", async (BechdelRepository repo) =>
{
  return Results.Ok(await repo.GetAll());
})
  .WithTags("Films:Read")
  .Produces(200);

group.MapGet("{id:regex(tt[0-9]*)}", async (BechdelRepository repo, string id) =>
{
  Console.WriteLine(id);
  var film = await repo.GetOne(id);
  if (film is null) return Results.NotFound("Couldn't find Film");
  return Results.Ok(film);
})
  .WithTags("Films:Read:Two")
  .Produces(200);

group.MapGet("{year:int}", async (BechdelRepository repo, int year, bool? passed) =>
{
  var results = await repo.GetByYear(year, passed);
  if (results.Any())
  {
    return Results.NoContent();
  }

  return Results.Ok(results);
})
  .WithTags("Films:Read:One")
  .Produces(200);

group.MapPost("", (BechdelRepository repo, Film model) =>
{
  return Results.Created($"/api/films/{model.IMDBId}", model);
})
  .WithTags("Films:Write")
  .Produces(201);

app.Run();

