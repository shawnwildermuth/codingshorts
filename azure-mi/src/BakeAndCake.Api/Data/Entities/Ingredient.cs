namespace BakeAndCake.Api.Data.Entities;

public class Ingredient
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public string Unit { get; set; } = string.Empty; // g / ml / piece
  public decimal CostPerUnit { get; set; }
  public decimal StockQuantity { get; set; }
  public decimal ReorderThreshold { get; set; }
  public bool IsAllergen { get; set; }
  public string? AllergenInfo { get; set; }

  // Navigation
  public ICollection<ProductIngredient> ProductIngredients { get; set; } = [];
  public ICollection<CustomPieIngredient> CustomPieIngredients { get; set; } = [];
}