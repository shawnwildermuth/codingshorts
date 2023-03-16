using DemoAPI.Data;
using DemoAPI.Models;
using WilderMinds.MinimalApiDiscovery;

namespace DemoAPI.Apis;

public class FilmsApi : IApi
{
  // private readonly BechdelRepository repo;

  // public FilmsApi(BechdelRepository repo)
  // {
  //   this.repo = repo;
  // }

  public void Register(WebApplication app)
  {
    var group = app.MapGroup("/api/films");

    group.MapGet("", async (BechdelRepository repo) =>
    {
      return Results.Ok(await repo.GetAll());
    })
      .Produces(200);

    group.MapGet("{id:regex(tt[0-9]*)}", 
      async (BechdelRepository repo, string id) =>
    {
      Console.WriteLine(id);
      var film = await repo.GetOne(id);
      if (film is null) return Results.NotFound("Couldn't find Film");
      return Results.Ok(film);
    })
      .Produces(200);

    group.MapGet("{year:int}", GetOne)
      .Produces(200);

    group.MapPost("", (Film model) =>
    {
      return Results.Created($"/api/films/{model.IMDBId}", model);
    })
      .Produces(201);

  }

  public async Task<IResult> GetOne(BechdelRepository repo, 
    int year, 
    bool? passed = false)
  {
    var results = await repo.GetByYear(year, passed);
    if (results.Count() == 0)
    {
      return Results.NoContent();
    }

    return Results.Ok(results);
  }
}