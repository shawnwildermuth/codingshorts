namespace TypedResultsApi.Data;

public class Job
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public DateTime PostedDate { get; set; }
}