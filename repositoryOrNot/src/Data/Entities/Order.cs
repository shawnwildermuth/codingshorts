namespace RepoOrNot.Data.Entities;

public class Order
{
  public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string OrderNumber { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public DateTime OrderDate { get; set; }
  public DateTime DueDate { get; set; }
  public string? Terms { get; set; }
  public int CustomerId { get; set; }

  public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}