namespace MappingTest.Models;

public class OrderItemModel
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public string? Description {get;set;}
  public string? Unit {get;set;}
  public decimal UnitPrice { get; set; }
  public int Quantity { get; set; }
  public double Discount { get; set; }
  public decimal LineTotal {get;set;}
}