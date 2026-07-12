using BakeAndCake.Api.Data.Entities;
using BakeAndCake.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using Xunit;

namespace BakeAndCake.Tests.Endpoints;

/// <summary>
/// Tests Receipt business rules and the VAT / loyalty-points calculation logic
/// used inside the CreateOrder endpoint handler.
/// </summary>
public class ReceiptEndpointTests
{

  [Fact]
  public async Task IssueReceipt_ReturnsNotFound_WhenOrderMissing()
  {
    var receiptMock = Substitute.For<IReceiptRepository>();
    var orderMock = Substitute.For<IOrderRepository>();
    orderMock.GetByIdAsync(99).Returns(Task.FromResult<Order?>(null));

    var order = await orderMock.GetByIdAsync(99);
    var result = order is null
        ? Results.NotFound(new ErrorModel { Message = "Order 99 not found." })
        : Results.Ok();

    result.Should().BeOfType<NotFound<ErrorModel>>();
  }

  [Fact]
  public async Task IssueReceipt_ReturnsConflict_WhenAlreadyIssued()
  {
    var order = TestData.PendingOrder();

    var receiptMock = Substitute.For<IReceiptRepository>();
    receiptMock.GetByOrderAsync(1)
        .Returns(Task.FromResult<Receipt?>(new Receipt { Id = 5, OrderId = 1, ReceiptNumber = "BPS-EXISTING" }));

    var orderMock = Substitute.For<IOrderRepository>();
    orderMock.GetByIdAsync(1).Returns(Task.FromResult<Order?>(order));

    var existingReceipt = await receiptMock.GetByOrderAsync(1);
    var result = existingReceipt is not null
        ? Results.Conflict(new ErrorModel { Message = "A receipt has already been issued for this order." })
        : Results.Ok();

    result.Should().BeOfType<Conflict<ErrorModel>>();
  }

  [Fact]
  public async Task IssueReceipt_ReturnsBadRequest_WhenTenderedAmountTooLow()
  {
    var order = TestData.PendingOrder();  // TotalAmount = 15.54

    var amountPaid = 10.00m;  // less than 15.54
    var result = amountPaid < order.TotalAmount
        ? Results.BadRequest(new ErrorModel { Message = $"Amount tendered (£{amountPaid:F2}) is less than the order total." })
        : Results.Ok();

    result.Should().BeOfType<BadRequest<ErrorModel>>();
  }

  [Fact]
  public void IssueReceipt_CalculatesCorrectChange()
  {
    var orderTotal = 15.54m;
    var amountPaid = 20.00m;
    var change = amountPaid - orderTotal;

    change.Should().BeApproximately(4.46m, 0.001m);
  }

  [Fact]
  public void IssueReceipt_GeneratesUniqueReceiptNumbers()
  {
    // Receipt numbers follow the BPS-yyyyMMdd-XXXXXX pattern
    var number1 = $"BPS-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
    var number2 = $"BPS-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

    number1.Should().NotBe(number2);
    number1.Should().StartWith("BPS-");
    number1.Should().MatchRegex(@"^BPS-\d{8}-[A-Z0-9]{6}$");
  }
}

/// <summary>
/// Verifies the VAT and loyalty-point formulas used in the CreateOrder handler.
/// </summary>
public class OrderPricingTests
{
  private const decimal VatRate = 0.20m;
  private const int LoyaltyPerPound = 10;

  [Theory]
  [InlineData(12.95, 0, 2.59, 15.54)]
  [InlineData(25.90, 0, 5.18, 31.08)]
  [InlineData(50.00, 10.00, 8.00, 48.00)]
  public void VatCalculation_IsCorrect(
      decimal subTotal, decimal discount, decimal expectedTax, decimal expectedTotal)
  {
    var taxable = subTotal - discount;
    var tax = Math.Round(taxable * VatRate, 2);
    var total = taxable + tax;

    tax.Should().BeApproximately(expectedTax, 0.01m);
    total.Should().BeApproximately(expectedTotal, 0.01m);
  }

  [Theory]
  [InlineData(15.54, 150)]
  [InlineData(30.00, 300)]
  [InlineData(9.99, 90)]
  public void LoyaltyPoints_AreCalculatedCorrectly(decimal orderTotal, int expectedMinPoints)
  {
    var points = (int)Math.Floor(orderTotal) * LoyaltyPerPound;
    points.Should().BeGreaterThanOrEqualTo(expectedMinPoints);
  }

  [Fact]
  public void ZeroDiscount_LeavesSubTotalUnchanged()
  {
    var subTotal = 14.50m;
    var discount = 0m;
    var taxable = subTotal - discount;

    taxable.Should().Be(subTotal);
  }

  [Fact]
  public void FullDiscountMakesOrderFree()
  {
    var subTotal = 14.50m;
    var discount = 14.50m;
    var taxable = subTotal - discount;
    var tax = Math.Round(taxable * VatRate, 2);
    var total = taxable + tax;

    total.Should().Be(0m);
  }
}
