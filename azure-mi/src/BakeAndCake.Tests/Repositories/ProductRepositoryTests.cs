using BakeAndCake.Api.Data.Entities;
using BakeAndCake.Api.Repositories;
using BakeAndCake.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace BakeAndCake.Tests.Repositories;

public class ProductRepositoryTests
{

  [Fact]
  public async Task GetAvailableAsync_ExcludesUnavailableProducts()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.AddRange(TestData.ApplePie(), TestData.SteakPie(), TestData.UnavailableProduct());
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var result = (await repo.GetAvailableAsync()).ToList();

    result.Should().HaveCount(2);
    result.Should().OnlyContain(p => p.IsAvailable);
  }


  [Fact]
  public async Task GetByCategoryAsync_ReturnsOnlyMatchingCategory()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.AddRange(TestData.ApplePie(), TestData.SteakPie(), TestData.UnavailableProduct());
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var result = (await repo.GetByCategoryAsync(ProductCategory.Pie)).ToList();

    result.Should().HaveCount(2);
    result.Should().OnlyContain(p => p.Category == ProductCategory.Pie);
  }

  [Fact]
  public async Task GetByCategoryAsync_ReturnsEmpty_WhenNoneInCategory()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.Add(TestData.ApplePie());
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var result = await repo.GetByCategoryAsync(ProductCategory.Bread);

    result.Should().BeEmpty();
  }


  [Fact]
  public async Task GetPieOfTheWeekAsync_ReturnsMarkedProduct()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.AddRange(TestData.ApplePie(), TestData.SteakPie()); // SteakPie has IsPieOfTheWeek = true
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var result = await repo.GetPieOfTheWeekAsync();

    result.Should().NotBeNull();
    result!.Name.Should().Be("Steak & Ale Pie");
  }

  [Fact]
  public async Task GetPieOfTheWeekAsync_ReturnsNull_WhenNoneMarked()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.Add(TestData.ApplePie());  // IsPieOfTheWeek = false
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var result = await repo.GetPieOfTheWeekAsync();

    result.Should().BeNull();
  }


  [Fact]
  public async Task SetAvailabilityAsync_MakesProductUnavailable()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.Add(TestData.ApplePie());
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var ok = await repo.SetAvailabilityAsync(1, false);

    ok.Should().BeTrue();
    db.Products.Find(1)!.IsAvailable.Should().BeFalse();
  }

  [Fact]
  public async Task SetAvailabilityAsync_ReturnsFalse_WhenProductNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new ProductRepository(db);
    var ok = await repo.SetAvailabilityAsync(999, false);

    ok.Should().BeFalse();
  }

  [Fact]
  public async Task AddAsync_PersistsProduct_WithIngredients()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.Add(TestData.Flour());
    await db.SaveChangesAsync(TestContext.Current.CancellationToken);

    var repo = new ProductRepository(db);
    var product = TestData.ApplePie();
    product.Id = 0;
    product.ProductIngredients =
    [
        new() { IngredientId = 1, QuantityRequired = 350m }
    ];

    var created = await repo.AddAsync(product);

    created.Id.Should().BeGreaterThan(0);
    db.ProductIngredients.Should().HaveCount(1);
  }


  [Fact]
  public async Task DeleteAsync_RemovesProduct_ReturnsTrue()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Products.Add(TestData.ApplePie());
    await db.SaveChangesAsync();

    var repo = new ProductRepository(db);
    var deleted = await repo.DeleteAsync(1);

    deleted.Should().BeTrue();
    db.Products.Should().BeEmpty();
  }

  [Fact]
  public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new ProductRepository(db);
    var deleted = await repo.DeleteAsync(99);

    deleted.Should().BeFalse();
  }
}
