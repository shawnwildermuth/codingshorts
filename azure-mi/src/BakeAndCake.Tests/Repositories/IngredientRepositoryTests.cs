using BakeAndCake.Api.Repositories;
using BakeAndCake.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace BakeAndCake.Tests.Repositories;

public class IngredientRepositoryTests
{

  [Fact]
  public async Task GetAllAsync_ReturnsAllIngredients_OrderedByName()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.AddRange(TestData.Apples(), TestData.Butter(), TestData.Flour());
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    var result = (await repo.GetAllAsync()).Select(i => i.Name).ToList();

    result.Should().BeInAscendingOrder();
    result.Should().HaveCount(3);
  }


  [Fact]
  public async Task GetLowStockAsync_ReturnsOnlyIngredientsBelowThreshold()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.AddRange(
        TestData.Flour(),            // 5000 stock, threshold 1000 → OK
        TestData.LowStockIngredient() // 50 stock, threshold 100 → LOW
    );
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    var result = (await repo.GetLowStockAsync()).ToList();

    result.Should().HaveCount(1);
    result[0].Name.Should().Be("Vanilla Extract");
  }

  [Fact]
  public async Task GetLowStockAsync_ReturnsEmpty_WhenAllStockSufficient()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.AddRange(TestData.Flour(), TestData.Butter());
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    var result = await repo.GetLowStockAsync();

    result.Should().BeEmpty();
  }


  [Fact]
  public async Task GetAllergensAsync_ReturnsOnlyAllergens()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.AddRange(TestData.Flour(), TestData.Butter(), TestData.Apples());
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    var result = (await repo.GetAllergensAsync()).ToList();

    result.Should().HaveCount(2);
    result.Should().OnlyContain(i => i.IsAllergen);
  }


  [Fact]
  public async Task AdjustStockAsync_IncreasesStock_WhenPositiveDelta()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.Add(TestData.Flour());  // 5000g
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    var ok = await repo.AdjustStockAsync(1, 1000m);

    ok.Should().BeTrue();
    db.Ingredients.Find(1)!.StockQuantity.Should().Be(6000m);
  }

  [Fact]
  public async Task AdjustStockAsync_DecreasesStock_WhenNegativeDelta()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.Add(TestData.Flour());  // 5000g
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    await repo.AdjustStockAsync(1, -500m);

    db.Ingredients.Find(1)!.StockQuantity.Should().Be(4500m);
  }

  [Fact]
  public async Task AdjustStockAsync_ClampsToZero_WhenStockWouldGoNegative()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.Add(TestData.Flour());  // 5000g
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    await repo.AdjustStockAsync(1, -999999m);

    db.Ingredients.Find(1)!.StockQuantity.Should().Be(0m);
  }

  [Fact]
  public async Task AdjustStockAsync_ReturnsFalse_WhenIngredientNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new IngredientRepository(db);
    var ok = await repo.AdjustStockAsync(999, 100m);

    ok.Should().BeFalse();
  }


  [Fact]
  public async Task AddAsync_PersistsIngredient()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new IngredientRepository(db);
    var ingredient = TestData.Flour();
    ingredient.Id = 0;

    var created = await repo.AddAsync(ingredient);

    created.Id.Should().BeGreaterThan(0);
    db.Ingredients.Should().HaveCount(1);
  }

  [Fact]
  public async Task DeleteAsync_RemovesIngredient_ReturnsTrue()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Ingredients.Add(TestData.Flour());
    await db.SaveChangesAsync();

    var repo = new IngredientRepository(db);
    var deleted = await repo.DeleteAsync(1);

    deleted.Should().BeTrue();
    db.Ingredients.Should().BeEmpty();
  }

  [Fact]
  public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new IngredientRepository(db);
    var deleted = await repo.DeleteAsync(99);

    deleted.Should().BeFalse();
  }
}
