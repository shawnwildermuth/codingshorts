namespace BakeAndCake.Api.Data.Entities;

public class OrderItem
{
  public int Id { get; set; }
  public int OrderId { get; set; }
  public Order Order { get; set; } = null!;

  // Either a catalogue product OR a custom pie — never both
  public int? ProductId { get; set; }
  public Product? Product { get; set; }
  public int? CustomPieId { get; set; }
  public CustomPie? CustomPie { get; set; }

  public int Quantity { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal LineTotal { get; set; }
  public string? SpecialRequests { get; set; }
}