namespace BechdelDataServer.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class BechdelFilms
{
  public string Source { get; set; }
  public IEnumerable<FilmYear> Years { get; set; }
}

public class FilmYear
{
  public int Year { get; set; }

  public IEnumerable<FilmInfo> Films { get; set; }
}

public class FilmInfo
{
  
  public string Title { get; set; }
  public string IMDBId { get; set; }
  public string ResultReason { get; set; }
  public bool Success { get; set; }
  public int Budget { get; set; }
  public int DomesticGross { get; set; }
  public int InternationalGross { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
