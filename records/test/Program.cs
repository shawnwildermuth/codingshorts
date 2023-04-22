using static System.Console;

WriteLine("Starting...");

var product = new Produce(1, "Peppers", 2.99m, DateTime.Today.AddDays(3));
var product2 = new Produce(1, "Peppers", 2.99m, DateTime.Today.AddDays(3));
WriteLine($"Equal? {product == product2}");
var product3 = new Produce(2, "Onions", 1.99m, DateTime.Today.AddDays(5));
var priceChange = product with { Price = 2.49m };

public abstract record Product(int Id,
  string Name,
  decimal Price,
  string? Description = null);

public record struct Produce(int Id,
  string Name,
  decimal Price,
  DateTime expiration,
  string? Description = null);// : Product(Id, Name, Price, Description);
