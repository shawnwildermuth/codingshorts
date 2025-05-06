namespace CSharpNew.Entities;

public partial class Product
{

  public partial Product()
  {
  }
  
  public partial string? GetProductInfo()
  {
    return Name;
  }

  public partial int OnHand
  {
    get
    {
      return this.Inventory.OnHand;
    }
  }

  public partial event Action<double> PriceChange
  {
    add { }
    remove { }
  }
}