namespace BakeAndCake.Api.Data.Entities;

public class Order
{
  public int Id { get; set; }
  public int? CustomerId { get; set; }
  public Customer? Customer { get; set; }
  public DateTime OrderDate { get; set; } = DateTime.UtcNow;
  public DateTime? RequiredByDate { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
  public FulfilmentType Fulfilment { get; set; } = FulfilmentType.InStore;
  public PaymentMethod PaymentMethod { get; set; }
  public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
  public decimal SubTotal { get; set; }
  public decimal DiscountAmount { get; set; }
  public decimal TaxAmount { get; set; }
  public decimal TotalAmount { get; set; }
  public string? DeliveryAddress { get; set; }
  public string? Notes { get; set; }
  public string? ServedBy { get; set; } // staff member

  // Navigation
  public ICollection<OrderItem> OrderItems { get; set; } = [];
  public Receipt? Receipt { get; set; }
}