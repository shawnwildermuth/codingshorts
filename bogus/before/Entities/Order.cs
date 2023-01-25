namespace FakingIt.Entities;

public class Order
{
  public int Id { get; set; }
  public DateTime OrderDate { get; set; }
  public string? Terms { get; set; }
  public int CustomerId { get; set; }
  public IEnumerable<OrderItem>? OrderItems {get;set;}
}