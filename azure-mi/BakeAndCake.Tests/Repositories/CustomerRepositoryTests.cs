using BakeAndCake.Api.Repositories;
using BakeAndCake.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace BakeAndCake.Tests.Repositories;

public class CustomerRepositoryTests
{

  [Fact]
  public async Task GetAllAsync_ReturnsAllCustomers()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.AddRange(TestData.Margaret(), TestData.Robert());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = (await repo.GetAllAsync()).ToList();

    result.Should().HaveCount(2);
  }

  [Fact]
  public async Task GetAllAsync_ReturnsEmptyList_WhenNoCustomers()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomerRepository(db);
    var result = await repo.GetAllAsync();

    result.Should().BeEmpty();
  }


  [Fact]
  public async Task GetByIdAsync_ReturnsCorrectCustomer()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = await repo.GetByIdAsync(1);

    result.Should().NotBeNull();
    result!.Email.Should().Be("margaret@test.com");
  }

  [Fact]
  public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomerRepository(db);
    var result = await repo.GetByIdAsync(999);

    result.Should().BeNull();
  }


  [Fact]
  public async Task GetByEmailAsync_ReturnsCustomer_WhenEmailMatches()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = await repo.GetByEmailAsync("margaret@test.com");

    result.Should().NotBeNull();
    result!.FirstName.Should().Be("Margaret");
  }

  [Fact]
  public async Task GetByEmailAsync_IsCaseInsensitive()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = await repo.GetByEmailAsync("MARGARET@TEST.COM");

    result.Should().NotBeNull();
  }

  [Fact]
  public async Task GetByEmailAsync_ReturnsNull_WhenEmailNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomerRepository(db);
    var result = await repo.GetByEmailAsync("ghost@nowhere.com");

    result.Should().BeNull();
  }


  [Fact]
  public async Task SearchAsync_ReturnsMatchingCustomers_ByLastName()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.AddRange(TestData.Margaret(), TestData.Robert());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = (await repo.SearchAsync("Whitfield")).ToList();

    result.Should().HaveCount(1);
    result[0].FirstName.Should().Be("Margaret");
  }

  [Fact]
  public async Task SearchAsync_ReturnsMatchingCustomers_ByEmail()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.AddRange(TestData.Margaret(), TestData.Robert());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = (await repo.SearchAsync("robert")).ToList();

    result.Should().HaveCount(1);
    result[0].LastName.Should().Be("Craine");
  }

  [Fact]
  public async Task SearchAsync_ReturnsEmpty_WhenNoMatch()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var result = await repo.SearchAsync("xyz123notfound");

    result.Should().BeEmpty();
  }


  [Fact]
  public async Task AddAsync_PersistsCustomer_AndAssignsId()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomerRepository(db);

    var customer = TestData.Margaret();
    customer.Id = 0;  // let EF assign

    var created = await repo.AddAsync(customer);

    created.Id.Should().BeGreaterThan(0);
    db.Customers.Should().HaveCount(1);
  }


  [Fact]
  public async Task UpdateAsync_ChangesPersistedCorrectly()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var repo = new CustomerRepository(db);
    var customer = (await repo.GetByIdAsync(1))!;
    customer.FirstName = "Maggie";

    var updated = await repo.UpdateAsync(customer);

    updated.FirstName.Should().Be("Maggie");
    db.Customers.Find(1)!.FirstName.Should().Be("Maggie");
  }


  [Fact]
  public async Task DeleteAsync_RemovesCustomer_ReturnsTrue()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    var deleted = await repo.DeleteAsync(1);

    deleted.Should().BeTrue();
    db.Customers.Should().BeEmpty();
  }

  [Fact]
  public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomerRepository(db);
    var deleted = await repo.DeleteAsync(999);

    deleted.Should().BeFalse();
  }


  [Fact]
  public async Task AdjustLoyaltyPointsAsync_AddsPoints_WhenPositive()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());  // starts at 200 pts
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    await repo.AdjustLoyaltyPointsAsync(1, 50);

    db.Customers.Find(1)!.LoyaltyPoints.Should().Be(250);
  }

  [Fact]
  public async Task AdjustLoyaltyPointsAsync_ReducesPoints_WhenNegative()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());  // 200 pts
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    await repo.AdjustLoyaltyPointsAsync(1, -100);

    db.Customers.Find(1)!.LoyaltyPoints.Should().Be(100);
  }

  [Fact]
  public async Task AdjustLoyaltyPointsAsync_ClampsAtZero_WhenOverRedeemed()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());  // 200 pts
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    await repo.AdjustLoyaltyPointsAsync(1, -9999);

    db.Customers.Find(1)!.LoyaltyPoints.Should().Be(0);
  }


  [Fact]
  public async Task ExistsAsync_ReturnsTrue_WhenCustomerExists()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomerRepository(db);
    (await repo.ExistsAsync(1)).Should().BeTrue();
  }

  [Fact]
  public async Task ExistsAsync_ReturnsFalse_WhenCustomerDoesNotExist()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomerRepository(db);
    (await repo.ExistsAsync(42)).Should().BeFalse();
  }
}
