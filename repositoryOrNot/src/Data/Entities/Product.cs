namespace RepoOrNot.Data.Entities;

public class Product
{
  public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string ProductName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public decimal UnitPrice { get; set; }
  public string? Unit { get; set; }
  public string? Description { get; set; }
  public string? Image { get; set; }
}
