using WrongAboutBlazor.Bechdel;

namespace WrongAboutBlazor.State;

public class AppState(BechdelClient client)
{
  public List<Film> Films { get; } = new();

  public List<int> Years { get; } = new();

  public int CurrentYear { get; set; } = 2013;

  public async Task<bool> LoadYears()
  {
    try
    {
      var result = await client.YearsAsync();

      if (result.Any())
      {
        Years.Clear();
        Years.AddRange(result);
        return true;
      }
    }
    catch { }

    return false;
  }

  public async Task<bool> LoadYear()
  {
    try
    {
      if (CurrentYear == default) return false;
      var result = await client.FilmsAsync(1, 50, CurrentYear);

      if (result.Results.Any())
      {
        Films.Clear();
        Films.AddRange(result.Results);
        return true;
      }
    }
    catch { }

    return false;

  }

  public async Task<bool> LoadAllFilms()
  {
    try
    {
      var result = await client.GetallnamesAsync(1, 25);

      if (result.Results.Any())
      {
        Films.Clear();
        Films.AddRange(result.Results);
        return true;
      }
    }
    catch { }

    return false;
  }
}