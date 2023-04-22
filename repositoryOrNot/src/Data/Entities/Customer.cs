namespace RepoOrNot.Data.Entities;

public class Customer
{
  public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string CompanyName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string? Phone { get; set; }
  public string? ContactName { get; set; }

  public ICollection<Order> Orders { get; set; } = new List<Order>();

}
