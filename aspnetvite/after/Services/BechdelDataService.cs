using System.Text.Json;
using BechdelDataServer.Models;

namespace BechdelDataServer.Services;

internal class BechdelDataService
{
  private readonly ILogger<BechdelDataService> _logger;
  private readonly IConfiguration _config;
  private IEnumerable<Film>? _data;

  public BechdelDataService(ILogger<BechdelDataService> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
  }

  public async Task<FilmResult> LoadFilmsByResultAndYearAsync(
    bool succeeded,
    int year,
    int page,
    int pageSize)
  {
    var data = await LoadAsync();

    if (data is null) return FilmResult.Default;

    IOrderedEnumerable<Film> qry = data
      .Where(y => y.Year == year)
      .Where(f => f.Passed == succeeded)
      .OrderBy(f => f.Title);

    return GetFilmResult(qry, page, pageSize);
  }

  public async Task<FilmResult> LoadFilmsByResultAsync(
    bool succeeded,
    int page,
    int pageSize)
  {
    var data = await LoadAsync();

    if (data is null) return FilmResult.Default;

    IOrderedEnumerable<Film> qry = data
      .Where(f => f.Passed == succeeded)
      .OrderBy(f => f.Title);

    return GetFilmResult(qry, page, pageSize);
  }

  public async Task<FilmResult> LoadAllFilmsAsync(int page, int pageSize)
  {
    var data = await LoadAsync();

    IOrderedEnumerable<Film> qry = data.OrderByDescending(d => d.Year)
                                       .ThenBy(f => f.Title);

    return GetFilmResult(qry, page, pageSize);
  }

  internal async Task<FilmResult> LoadAllFilmsByYearAsync(int page, int pageSize, int year)
  {
    var data = await LoadAsync();

    IOrderedEnumerable<Film> qry = data.Where(d => d.Year == year)
                                       .OrderByDescending(d => d.Year)
                                       .ThenBy(f => f.Title);

    return GetFilmResult(qry, page, pageSize);
  }

  FilmResult GetFilmResult(IOrderedEnumerable<Film> query, int page, int pageSize)
  {
    var count = query.Count();
    var pageCount = (int)Math.Ceiling(((double)count / (double)pageSize));
    var results = query.Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();

    return new FilmResult(count, pageCount, page, results);
  }

  protected async Task<IEnumerable<Film>> LoadAsync()
  {
    if (_data is null)
    {
      var opts = new JsonSerializerOptions()
      {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };

      _logger.LogInformation("Loading From Data Json");
      var json = await File.ReadAllTextAsync("./bechdel-films.json");
      var theFilms = JsonSerializer.Deserialize<List<Film>>(json, opts);
      if (theFilms is null) throw new InvalidDataException("Failed to read the data");
      _data = theFilms;
      _logger.LogInformation("Loaded From Data Json");
    }

    return _data;
  }

  internal async Task<int[]?> LoadFilmYears()
  {
    var data = await LoadAsync();
    if (data is null) return null;

    var results = data.OrderByDescending(d => d.Year).Select(d => d.Year).Distinct().ToArray();
    return results;
  }
}
