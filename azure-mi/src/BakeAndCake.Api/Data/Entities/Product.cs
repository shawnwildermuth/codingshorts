namespace BakeAndCake.Api.Data.Entities;

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public string? ShortDescription { get; set; }
  public decimal Price { get; set; }
  public ProductCategory Category { get; set; }
  public bool IsAvailable { get; set; } = true;
  public bool IsPieOfTheWeek { get; set; }
  public string? ImageUrl { get; set; }
  public string? AllergyInformation { get; set; }
  public int PreparationTimeMinutes { get; set; }

  // Navigation
  public ICollection<ProductIngredient> ProductIngredients { get; set; } = [];
  public ICollection<OrderItem> OrderItems { get; set; } = [];
}