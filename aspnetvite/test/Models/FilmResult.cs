namespace BechdelDataServer.Models;

public record FilmResult(int Count, 
    int PageCount, 
    int CurrentPage, 
    IEnumerable<Film>? Results)
{
  public static FilmResult Default = new FilmResult(0, 0, 0, null);
}
