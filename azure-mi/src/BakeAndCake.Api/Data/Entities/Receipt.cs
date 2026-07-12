namespace BakeAndCake.Api.Data.Entities;

public class Receipt
{
  public int Id { get; set; }
  public int OrderId { get; set; }
  public Order Order { get; set; } = null!;
  public string ReceiptNumber { get; set; } = string.Empty;
  public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
  public decimal AmountPaid { get; set; }
  public decimal ChangeGiven { get; set; }
}