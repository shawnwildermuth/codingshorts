namespace BechdelDataServer.Models
{
  /// <summary>
  /// Represents a single Film
  /// </summary>
  public class Film
  {
    /// <summary>
    /// Year of Release
    /// </summary>
    public int Year { get; set; }
    /// <summary>
    /// Film Title
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// IMDB Identifier
    /// </summary>
    public string? IMDBId { get; set; }
    /// <summary>
    /// Reason for passed or failure
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// Whether the film passed the Bechdel test.
    /// </summary>
    public bool Passed { get; set; }
    /// <summary>
    /// The Budget in US Dollars
    /// </summary>
    public int Budget { get; set; }
    /// <summary>
    /// The Domestic Gross Sales in US Dollars
    /// </summary>
    public int DomesticGross { get; set; }
    /// <summary>
    /// The International Gross Sales in US Dollars
    /// </summary>
    public int InternationalGross { get; set; }

    internal bool InfoLoaded { get; set;}
    
    /// <summary>
    /// A link to a poster image (supplied by themoviedb.org)
    /// </summary>
    public string? PosterUrl { get; set; }

    /// <summary>
    /// A film summary
    /// </summary>
    public string? Overview { get; set; }

    /// <summary>
    /// Film Rating via IMDB
    /// </summary>
    public float Rating { get; set; }

  }
}
