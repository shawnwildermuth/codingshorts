using System.Text.Json;
using DemoAPI.Models;

namespace DemoAPI.Data;

public class BechdelRepository
{
  private IEnumerable<Film>? _films = null;

  public async Task<IEnumerable<Film>> GetAll()
  {
    return await GetFilms();
  }

  public async Task<IEnumerable<Film>> GetByYear(int year, bool? passed = null)
  {
    return (await GetFilms())
      .Where(f => f.Year == year && (passed is null ? true : f.Passed == passed))
      .OrderBy(f => f.Title)
      .ToList();
  }

  public async Task<IEnumerable<Film>> GetByPassed()
  {
    return (await GetFilms())
      .Where(f => f.Passed)
      .OrderByDescending(f => f.Year)
      .ThenBy(f => f.Title)
      .ToList();
  }

  public async Task<IEnumerable<Film>> GetByFailed()
  {
    return (await GetFilms())
      .Where(f => !f.Passed)
      .OrderByDescending(f => f.Year)
      .ThenBy(f => f.Title)
      .ToList();
  }

  public async Task<Film?> GetOne(string id)
  {
    var films = await GetFilms();
    return films.Where(f => f.IMDBId == id)
      .FirstOrDefault();
  }

  private async Task<IEnumerable<Film>> GetFilms()
  {
    if (_films is null)
    {
      var json = await File.ReadAllTextAsync("bechdel-films.json");
      _films = JsonSerializer.Deserialize<IEnumerable<Film>>(json);
    }

    return _films ?? new List<Film>();
  }
}