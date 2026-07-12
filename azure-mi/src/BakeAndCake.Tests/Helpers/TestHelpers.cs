using BakeAndCake.Api.Data;
using BakeAndCake.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Tests.Helpers;

/// <summary>
/// Creates a fresh, isolated in-memory EF Core context per test.
/// </summary>
public static class DbContextFactory
{
  public static BakeAndCakeDbContext CreateInMemory(string? dbName = null)
  {
    var options = new DbContextOptionsBuilder<BakeAndCakeDbContext>()
        .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
        .Options;

    return new BakeAndCakeDbContext(options);
  }
}

/// <summary>
/// Pre-built object graphs used across multiple test classes.
/// </summary>
public static class TestData
{

  public static Ingredient Flour() => new()
  {
    Id = 1,
    Name = "Plain Flour",
    Unit = "g",
    CostPerUnit = 0.0015m,
    StockQuantity = 5000,
    ReorderThreshold = 1000,
    IsAllergen = true,
    AllergenInfo = "Gluten"
  };

  public static Ingredient Butter() => new()
  {
    Id = 2,
    Name = "Butter",
    Unit = "g",
    CostPerUnit = 0.012m,
    StockQuantity = 2000,
    ReorderThreshold = 500,
    IsAllergen = true,
    AllergenInfo = "Dairy"
  };

  public static Ingredient Apples() => new()
  {
    Id = 3,
    Name = "Bramley Apples",
    Unit = "g",
    CostPerUnit = 0.004m,
    StockQuantity = 3000,
    ReorderThreshold = 600,
    IsAllergen = false
  };

  public static Ingredient LowStockIngredient() => new()
  {
    Id = 4,
    Name = "Vanilla Extract",
    Unit = "ml",
    CostPerUnit = 0.05m,
    StockQuantity = 50,
    ReorderThreshold = 100,
    IsAllergen = false
  };


  public static Customer Margaret() => new()
  {
    Id = 1,
    FirstName = "Margaret",
    LastName = "Whitfield",
    Email = "margaret@test.com",
    Phone = "07700900001",
    IsLoyaltyMember = true,
    LoyaltyPoints = 200,
    CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
  };

  public static Customer Robert() => new()
  {
    Id = 2,
    FirstName = "Robert",
    LastName = "Craine",
    Email = "robert@test.com",
    Phone = null,
    IsLoyaltyMember = false,
    LoyaltyPoints = 0,
    CreatedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
  };


  public static Product ApplePie() => new()
  {
    Id = 1,
    Name = "Classic Apple Pie",
    Price = 12.95m,
    Category = ProductCategory.Pie,
    IsAvailable = true,
    IsPieOfTheWeek = false,
    PreparationTimeMinutes = 60,
    ProductIngredients = []
  };

  public static Product SteakPie() => new()
  {
    Id = 2,
    Name = "Steak & Ale Pie",
    Price = 14.50m,
    Category = ProductCategory.Pie,
    IsAvailable = true,
    IsPieOfTheWeek = true,
    PreparationTimeMinutes = 90,
    ProductIngredients = []
  };

  public static Product UnavailableProduct() => new()
  {
    Id = 3,
    Name = "Seasonal Special",
    Price = 9.99m,
    Category = ProductCategory.Tart,
    IsAvailable = false,
    IsPieOfTheWeek = false,
    PreparationTimeMinutes = 30,
    ProductIngredients = []
  };


  public static CustomPie PendingCustomPie(int customerId = 1) => new()
  {
    Id = 1,
    CustomerId = customerId,
    Name = "Mum's Birthday Pie",
    Size = PieSize.Large,
    CrustType = PastryCrust.ShortCrust,
    PrimaryFilling = FillingType.Fruit,
    IsApproved = false,
    EstimatedPrice = 0m,
    CreatedAt = DateTime.UtcNow,
    CustomPieIngredients = []
  };

  public static CustomPie ApprovedCustomPie(int customerId = 1) => new()
  {
    Id = 2,
    CustomerId = customerId,
    Name = "Anniversary Tart",
    Size = PieSize.Medium,
    CrustType = PastryCrust.PuffPastry,
    PrimaryFilling = FillingType.Chocolate,
    IsApproved = true,
    EstimatedPrice = 22.50m,
    CreatedAt = DateTime.UtcNow,
    CustomPieIngredients = []
  };


  public static Order PendingOrder(int? customerId = 1) => new()
  {
    Id = 1,
    CustomerId = customerId,
    OrderDate = DateTime.UtcNow,
    Status = OrderStatus.Pending,
    PaymentStatus = PaymentStatus.Unpaid,
    PaymentMethod = PaymentMethod.Card,
    Fulfilment = FulfilmentType.InStore,
    SubTotal = 12.95m,
    DiscountAmount = 0m,
    TaxAmount = 2.59m,
    TotalAmount = 15.54m,
    OrderItems = []
  };
}
