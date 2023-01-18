namespace BechdelDataServer.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class TmdbResult
{
  public Movie_Results[] movie_results { get; set; }
  public object[] person_results { get; set; }
  public object[] tv_results { get; set; }
  public object[] tv_episode_results { get; set; }
  public object[] tv_season_results { get; set; }
}

public class Movie_Results
{
  public bool adult { get; set; }
  public string backdrop_path { get; set; }
  public int[] genre_ids { get; set; }
  public string original_language { get; set; }
  public string original_title { get; set; }
  public string poster_path { get; set; }
  public int id { get; set; }
  public bool video { get; set; }
  public string title { get; set; }
  public string overview { get; set; }
  public string release_date { get; set; }
  public int vote_count { get; set; }
  public float vote_average { get; set; }
  public float popularity { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
