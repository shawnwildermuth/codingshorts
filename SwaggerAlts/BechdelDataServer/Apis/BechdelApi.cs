using MinimalApis.Discovery;
using static Microsoft.AspNetCore.Http.Results;
namespace BechdelDataServer.Apis;

public class BechdelApi : IApi
{
  public void Register(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/api/films");

    group.MapGet("", GetAllFilms)
      .Produces<IEnumerable<Film>>()
      .Produces(404)
      .ProducesProblem(500)
      .WithName("getallnames")
      .WithOpenApi();

    group.MapGet("{year:int}", GetFilmByYear)
      .Produces<IEnumerable<Film>>()
      .Produces(404)
      .ProducesProblem(500)
      .WithOpenApi();

    group.MapGet("failed", GetFailedFilms)
      .Produces<IEnumerable<Film>>()
      .Produces(404)
      .ProducesProblem(500)
      .WithOpenApi();

    group.MapGet("passed", GetPassedFilms)
      .Produces<IEnumerable<Film>>()
      .Produces(404)
      .ProducesProblem(500);

    group.MapGet("failed/{year:int}", GetFailedFilmsByYear)
      .Produces<IEnumerable<Film>>()
      .Produces(404)
      .ProducesProblem(500);

    group.MapGet("passed/{year:int}", GetPassedFilmsByYear)
      .Produces<IEnumerable<Film>>()
      .Produces(404)
      .ProducesProblem(500);

    app.MapGet("api/years", GetYears)
      .Produces<int[]>().Produces<string[]>(200)
      .ProducesProblem(500);

  }

  public static async Task<IResult> GetAllFilms(BechdelDataService ds, int? page, int? pageSize)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    int pageNumber = page ?? 1;
    int pagerTake = pageSize ?? 50;
    FilmResult data = await ds.LoadAllFilmsAsync(pageNumber, pagerTake);
    if (data.Results is null) return NotFound();
    return Ok(data);
  }

  public static async Task<IResult> GetFilmByYear(BechdelDataService ds, int? page, int? pageSize, int year)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    int pageNumber = page ?? 1;
    int pagerTake = pageSize ?? 50;
    FilmResult data = await ds.LoadAllFilmsByYearAsync(pageNumber, pagerTake, year);
    if (data.Results is null) return NotFound();
    return Ok(data);
  }

  public static async Task<IResult> GetFailedFilms(BechdelDataService ds, int? page, int? pageSize)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    int pageNumber = page ?? 1;
    int pagerTake = pageSize ?? 50;
    FilmResult data = await ds.LoadFilmsByResultAsync(false, pageNumber, pagerTake);
    if (data.Results is null) return NotFound();
    return Ok(data);
  }

  public static async Task<IResult> GetPassedFilms(BechdelDataService ds, int? page, int? pageSize)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    int pageNumber = page ?? 1;
    int pagerTake = pageSize ?? 50;
    FilmResult data = await ds.LoadFilmsByResultAsync(true, pageNumber, pagerTake);
    if (data.Results is null) return NotFound();
    return Ok(data);
  }

  public static async Task<IResult> GetFailedFilmsByYear(BechdelDataService ds, int year, int? page, int? pageSize)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    int pageNumber = page ?? 1;
    int pagerTake = pageSize ?? 50;
    FilmResult data = await ds.LoadFilmsByResultAndYearAsync(false, year, pageNumber, pagerTake);
    if (data.Results is null) NotFound();
    return Ok(data);
  }

  public static async Task<IResult> GetPassedFilmsByYear(BechdelDataService ds, int year, int? page, int? pageSize)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    int pageNumber = page ?? 1;
    int pagerTake = pageSize ?? 50;
    FilmResult data = await ds.LoadFilmsByResultAndYearAsync(true, year, pageNumber, pagerTake);
    if (data.Results is null) NotFound();
    return Ok(data);
  }

  public static async Task<IResult> GetYears(BechdelDataService ds)
  {
    if (ds is null) return Problem("Server Error", statusCode: 500);
    var data = await ds.LoadFilmYears();
    if (data is null) NotFound();
    return Ok(data);
  }
}
