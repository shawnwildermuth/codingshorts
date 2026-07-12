using BakeAndCake.Api.Data.Entities;
using BakeAndCake.Api.Repositories;
using BakeAndCake.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace BakeAndCake.Tests.Repositories;

// ════════════════════════════════════════════════════════════════════════════
//  CUSTOM PIE
// ════════════════════════════════════════════════════════════════════════════

public class CustomPieRepositoryTests
{
  [Fact]
  public async Task GetPendingApprovalAsync_ReturnsOnlyUnapprovedPies()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.CustomPies.AddRange(TestData.PendingCustomPie(), TestData.ApprovedCustomPie());
    await db.SaveChangesAsync();

    var repo = new CustomPieRepository(db);
    var result = (await repo.GetPendingApprovalAsync()).ToList();

    result.Should().HaveCount(1);
    result[0].IsApproved.Should().BeFalse();
  }

  [Fact]
  public async Task GetByCustomerAsync_ReturnsOnlyPiesForThatCustomer()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.AddRange(TestData.Margaret(), TestData.Robert());

    var pieForMargaret = TestData.PendingCustomPie(customerId: 1);
    var pieForRobert = TestData.PendingCustomPie(customerId: 2);
    pieForRobert.Id = 10;

    db.CustomPies.AddRange(pieForMargaret, pieForRobert);
    await db.SaveChangesAsync();

    var repo = new CustomPieRepository(db);
    var result = (await repo.GetByCustomerAsync(1)).ToList();

    result.Should().HaveCount(1);
    result[0].CustomerId.Should().Be(1);
  }

  [Fact]
  public async Task ApproveAsync_SetsApprovedAndPrice()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.CustomPies.Add(TestData.PendingCustomPie());
    await db.SaveChangesAsync();

    var repo = new CustomPieRepository(db);
    var ok = await repo.ApproveAsync(1, 24.99m);

    ok.Should().BeTrue();
    var pie = db.CustomPies.Find(1)!;
    pie.IsApproved.Should().BeTrue();
    pie.EstimatedPrice.Should().Be(24.99m);
  }

  [Fact]
  public async Task ApproveAsync_ReturnsFalse_WhenPieNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new CustomPieRepository(db);
    var ok = await repo.ApproveAsync(999, 10m);

    ok.Should().BeFalse();
  }

  [Fact]
  public async Task AddAsync_PersistsCustomPie()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    await db.SaveChangesAsync();

    var repo = new CustomPieRepository(db);
    var pie = TestData.PendingCustomPie();
    pie.Id = 0;

    var created = await repo.AddAsync(pie);

    created.Id.Should().BeGreaterThan(0);
    db.CustomPies.Should().HaveCount(1);
  }

  [Fact]
  public async Task DeleteAsync_RemovesCustomPie_ReturnsTrue()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.CustomPies.Add(TestData.PendingCustomPie());
    await db.SaveChangesAsync();

    var repo = new CustomPieRepository(db);
    var deleted = await repo.DeleteAsync(1);

    deleted.Should().BeTrue();
    db.CustomPies.Should().BeEmpty();
  }

  [Fact]
  public async Task ExistsAsync_ReturnsTrue_WhenPieExists()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.CustomPies.Add(TestData.PendingCustomPie());
    await db.SaveChangesAsync();

    var repo = new CustomPieRepository(db);
    (await repo.ExistsAsync(1)).Should().BeTrue();
  }
}

// ════════════════════════════════════════════════════════════════════════════
//  ORDER
// ════════════════════════════════════════════════════════════════════════════

public class OrderRepositoryTests
{
  [Fact]
  public async Task GetByStatusAsync_ReturnsOnlyOrdersWithMatchingStatus()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());

    var pending = TestData.PendingOrder(customerId: 1);
    var confirmed = TestData.PendingOrder(customerId: 1);
    confirmed.Id = 2;
    confirmed.Status = OrderStatus.Confirmed;

    db.Orders.AddRange(pending, confirmed);
    await db.SaveChangesAsync();

    var repo = new OrderRepository(db);
    var result = (await repo.GetByStatusAsync(OrderStatus.Pending)).ToList();

    result.Should().HaveCount(1);
    result[0].Status.Should().Be(OrderStatus.Pending);
  }

  [Fact]
  public async Task UpdateStatusAsync_ChangesOrderStatus()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.Orders.Add(TestData.PendingOrder());
    await db.SaveChangesAsync();

    var repo = new OrderRepository(db);
    var ok = await repo.UpdateStatusAsync(1, OrderStatus.Baking);

    ok.Should().BeTrue();
    db.Orders.Find(1)!.Status.Should().Be(OrderStatus.Baking);
  }

  [Fact]
  public async Task UpdateStatusAsync_ReturnsFalse_WhenNotFound()
  {
    await using var db = DbContextFactory.CreateInMemory();
    var repo = new OrderRepository(db);
    var ok = await repo.UpdateStatusAsync(999, OrderStatus.Baking);

    ok.Should().BeFalse();
  }

  [Fact]
  public async Task UpdatePaymentAsync_UpdatesPaymentDetails()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.Orders.Add(TestData.PendingOrder());
    await db.SaveChangesAsync();

    var repo = new OrderRepository(db);
    var ok = await repo.UpdatePaymentAsync(1, PaymentStatus.Paid, PaymentMethod.Cash);

    ok.Should().BeTrue();
    var order = db.Orders.Find(1)!;
    order.PaymentStatus.Should().Be(PaymentStatus.Paid);
    order.PaymentMethod.Should().Be(PaymentMethod.Cash);
  }

  [Fact]
  public async Task GetDailyRevenueAsync_SumsOnlyPaidOrdersForDate()
  {
    await using var db = DbContextFactory.CreateInMemory();

    var today = DateTime.UtcNow.Date;
    var paidToday = new Order
    {
      Id = 1,
      OrderDate = today.AddHours(9),
      TotalAmount = 30m,
      PaymentStatus = PaymentStatus.Paid,
      PaymentMethod = PaymentMethod.Card,
      OrderItems = []
    };
    var unpaidToday = new Order
    {
      Id = 2,
      OrderDate = today.AddHours(10),
      TotalAmount = 15m,
      PaymentStatus = PaymentStatus.Unpaid,
      PaymentMethod = PaymentMethod.Cash,
      OrderItems = []
    };
    var paidYesterday = new Order
    {
      Id = 3,
      OrderDate = today.AddDays(-1),
      TotalAmount = 50m,
      PaymentStatus = PaymentStatus.Paid,
      PaymentMethod = PaymentMethod.Card,
      OrderItems = []
    };

    db.Orders.AddRange(paidToday, unpaidToday, paidYesterday);
    await db.SaveChangesAsync();

    var repo = new OrderRepository(db);
    var revenue = await repo.GetDailyRevenueAsync(today);

    revenue.Should().Be(30m);
  }

  [Fact]
  public async Task AddAsync_PersistsOrder_WithItems()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());
    db.Products.Add(TestData.ApplePie());
    await db.SaveChangesAsync();

    var repo = new OrderRepository(db);
    var order = TestData.PendingOrder();
    order.Id = 0;
    order.OrderItems =
    [
        new() { ProductId = 1, Quantity = 2, UnitPrice = 12.95m, LineTotal = 25.90m }
    ];

    var created = await repo.AddAsync(order);

    created.Id.Should().BeGreaterThan(0);
    db.OrderItems.Should().HaveCount(1);
  }

  [Fact]
  public async Task DeleteAsync_RemovesOrder_AndCascadesItems()
  {
    await using var db = DbContextFactory.CreateInMemory();
    db.Customers.Add(TestData.Margaret());

    var order = TestData.PendingOrder();
    order.OrderItems =
    [
        new() { ProductId = null, CustomPieId = null, Quantity = 1,
                    UnitPrice = 12.95m, LineTotal = 12.95m }
    ];
    db.Orders.Add(order);
    await db.SaveChangesAsync();

    var repo = new OrderRepository(db);
    var deleted = await repo.DeleteAsync(1);

    deleted.Should().BeTrue();
    db.Orders.Should().BeEmpty();
    db.OrderItems.Should().BeEmpty();  // cascade delete
  }
}
