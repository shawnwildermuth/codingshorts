namespace ShoeMoney.Data.Entities;

public class Product()
{
  public int Id { get; set; }
  public required string? Gender { get; set; }
  public int CategoryId { get; set; }
  public Category? Category { get; set; }
  public required string Type { get; set; }
  public required string Color { get; set; }
  public required string Usage { get; set; }
  public required string Title { get; set; }
  public string? ImageFile { get; set; }
  public string? ImageUrl { get; set; }
  public decimal Price { get; set; } = 0m;
}
