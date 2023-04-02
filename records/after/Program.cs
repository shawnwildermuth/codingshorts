using static System.Console;
using Foo;

WriteLine("Starting...");

var product = new RetailProduct(1, "Tomato", 2.99m, null, 10);
var product2 = new RetailProduct(1, "Tomato", 2.99m, "A red tomato", 5);
var product3 = product with { Price = 1.99m };

WriteLine($"Are Equal? {product == product2}");
WriteLine(product.ToString());

namespace Foo
{
  public abstract record Product(int Id,
                        string Name,
                        decimal Price,
                        string? Description = null)
  {
    public bool IsLowPrice() => Price < 100;
  };

  public record RetailProduct(int Id,
    string Name,
    decimal Price,
    string? Description,
    int NumInStock) : Product(Id, Name, Price, Description);
}