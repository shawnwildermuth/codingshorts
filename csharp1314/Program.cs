using static System.Console;
using System.Linq;
using CSharpNew.Entities;
using CSharpNew;

Clear();

PrintThese("One", "Two", "Three");

void PrintThese(params List<string> items)
{
  items.Sort();

  foreach (var item in items)
  {
    WriteLine(item);
  }
}

var product = new Product()
{
  Name = "Apple Sauce"
};

product = null;

product?.Price = 3.99;

WriteLine($"{product?.Name} + {product?.Price}");