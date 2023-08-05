namespace MappingTest.Models;

public class OrderModel
{
  public int Id { get; set; }
  public DateTime OrderDate { get; set; }
  public string? Terms { get; set; }
  public int CustomerId { get; set; }
  public IEnumerable<OrderItemModel>? OrderItems {get;set;}
}