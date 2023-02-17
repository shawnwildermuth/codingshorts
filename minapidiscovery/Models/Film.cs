namespace DemoAPI.Models;

public class Film
{
  public int Year { get; set; }
  public string? Title { get; set; }
  public string? IMDBId { get; set; }
  public string? Reason { get; set; }
  public bool Passed { get; set; }
  public int Budget { get; set; }
  public int DomesticGross { get; set; }
  public int InternationalGross { get; set; }
  public string? PosterUrl { get; set; }
  public string? Overview { get; set; }
  public float Rating { get; set; }
}
