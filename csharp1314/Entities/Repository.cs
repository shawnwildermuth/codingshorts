using Bogus;

namespace CSharpNew.Entities;

public class Repository
{
  public List<Product> GetProducts()
  {
    Randomizer.Seed = new Random(1773);

    // Faker for Location
    var locationFaker = new Faker<Location>()
        .RuleFor(l => l.Id, f => f.IndexFaker)
        .RuleFor(l => l.Name, f => f.Address.City());

    // Faker for Inventory
    var inventoryFaker = new Faker<Inventory>()
        .RuleFor(i => i.OnHand, f => f.Random.Int(0, 100))
        .RuleFor(i => i.OnBackorder, f => f.Random.Int(0, 100))
        .RuleFor(i => i.OnOrder, f => f.Random.Int(0, 100))
        .RuleFor(i => i.Store, f => locationFaker.Generate());

    // Faker for Category
    var categoryFaker = new Faker<Category>()
        .RuleFor(l => l.Id, f => f.IndexFaker)
        .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

    // Faker for Product
    var productFaker = new Faker<Product>()
        .RuleFor(l => l.Id, f => f.IndexFaker)
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => double.Parse(f.Commerce.Price()))
        .RuleFor(p => p.Category, f => categoryFaker.Generate())
        .RuleFor(p => p.Inventory, f => inventoryFaker.Generate());

    // Generate a list of products
    return productFaker.Generate(10); // Generate 10 products
  }
}