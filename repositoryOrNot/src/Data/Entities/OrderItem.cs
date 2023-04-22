namespace RepoOrNot.Data.Entities;

public class OrderItem
{
  public int Id { get; set; }
  public string ProductName { get; set; } = "";
  public double Quantity { get; set; }
  public double UnitPrice { get; set; }
  public double Discount { get; set; }
  public string? Notes { get; set; }

  public int OrderId { get; set; }
}