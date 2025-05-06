namespace CSharpNew.Entities;

public partial class Product
{
  public partial Product();

  public int Id { get; set; }
  public required string Name { get; set; }

  public double Price
  {
    get
    {
      return field + 1;
    }
    set
    {
      field = value;
    }
  }
  
  public Category? Category { get; set; }
  public Inventory? Inventory { get; set; }

  public partial string? GetProductInfo();

  public partial int OnHand { get; }

  public partial event Action<double> PriceChange;
}

