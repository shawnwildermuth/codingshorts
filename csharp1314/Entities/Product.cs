namespace CSharpNew.Entities;

public partial class Product
{
  public Product() {}

  public int Id { get; set; }
  public required string Name { get; set; }
  public double Price { get; set; }
  public Category? Category { get; set; }
  public Inventory? Inventory { get; set; }
}

