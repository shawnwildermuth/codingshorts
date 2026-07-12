using Mapster;

namespace BakeAndCake.Api.Endpoints;

public static class OrderEndpoints
{
  private const decimal VatRate = 0.20m;  // UK standard VAT
  private const int LoyaltyPerPound = 10;   // 10 points per £1 spent

  public static void MapOrderEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/api/orders").WithTags("Orders");

    group.MapGet("/", async (IOrderRepository repo) =>
        Results.Ok((await repo.GetAllAsync()).Select(o => o.Adapt<OrderModel>())))
        .WithName("GetAllOrders");

    group.MapGet("/{id:int}", async (int id, IOrderRepository repo) =>
    {
      var order = await repo.GetWithItemsAsync(id);
      return order is null ? Results.NotFound() : Results.Ok(order.Adapt<OrderModel>());
    })
    .WithName("GetOrderById");

    group.MapGet("/status/{status}", async (string status, IOrderRepository repo) =>
    {
      if (!Enum.TryParse<OrderStatus>(status, ignoreCase: true, out var s))
        return Results.BadRequest(new { message = $"Unknown order status '{status}'." });
      return Results.Ok((await repo.GetByStatusAsync(s)).Select(o => o.Adapt<OrderModel>()));
    })
    .WithName("GetOrdersByStatus");

    group.MapGet("/customer/{customerId:int}", async (int customerId, IOrderRepository repo) =>
        Results.Ok((await repo.GetByCustomerAsync(customerId)).Select(o => o.Adapt<OrderModel>())))
        .WithName("GetOrdersByCustomer");

    // GET /api/orders/revenue?date=2025-12-24
    group.MapGet("/revenue", async (DateTime date, IOrderRepository repo) =>
        Results.Ok(new
        {
          date = date.Date,
          revenue = await repo.GetDailyRevenueAsync(date)
        }))
        .WithName("GetDailyRevenue")
        .WithSummary("Total paid revenue for a given date");

    group.MapPost("/", async (
        CreateOrderModel dto,
        IOrderRepository orderRepo,
        IProductRepository productRepo,
        ICustomPieRepository customPieRepo,
        ICustomerRepository customerRepo) =>
    {
      var items = new List<OrderItem>();

      foreach (var lineDto in dto.Items)
      {
        if (lineDto.ProductId is null && lineDto.CustomPieId is null)
          return Results.BadRequest(new { message = "Each order item must reference a ProductId or a CustomPieId." });

        decimal unitPrice = 0m;

        if (lineDto.ProductId.HasValue)
        {
          var product = await productRepo.GetByIdAsync(lineDto.ProductId.Value);
          if (product is null)
            return Results.BadRequest(new { message = $"Product {lineDto.ProductId} not found." });
          if (!product.IsAvailable)
            return Results.BadRequest(new { message = $"Product '{product.Name}' is currently unavailable." });
          unitPrice = product.Price;
        }
        else if (lineDto.CustomPieId.HasValue)
        {
          var customPie = await customPieRepo.GetByIdAsync(lineDto.CustomPieId.Value);
          if (customPie is null)
            return Results.BadRequest(new { message = $"Custom pie {lineDto.CustomPieId} not found." });
          if (!customPie.IsApproved)
            return Results.BadRequest(new { message = $"Custom pie '{customPie.Name}' has not yet been approved." });
          unitPrice = customPie.EstimatedPrice;
        }

        items.Add(new OrderItem
        {
          ProductId = lineDto.ProductId,
          CustomPieId = lineDto.CustomPieId,
          Quantity = lineDto.Quantity,
          UnitPrice = unitPrice,
          LineTotal = unitPrice * lineDto.Quantity,
          SpecialRequests = lineDto.SpecialRequests
        });
      }

      var subTotal = items.Sum(i => i.LineTotal);
      var discount = dto.DiscountAmount;
      var taxable = subTotal - discount;
      var tax = Math.Round(taxable * VatRate, 2);
      var total = taxable + tax;

      var order = dto.Adapt<Order>();
      order.SubTotal = subTotal;
      order.TaxAmount = tax;
      order.TotalAmount = total;
      order.OrderItems = items;

      var created = await orderRepo.AddAsync(order);

      // Award loyalty points for loyalty members
      if (dto.CustomerId.HasValue)
      {
        var customer = await customerRepo.GetByIdAsync(dto.CustomerId.Value);
        if (customer is { IsLoyaltyMember: true })
          await customerRepo.AdjustLoyaltyPointsAsync(dto.CustomerId.Value,
                  (int)Math.Floor(total) * LoyaltyPerPound);
      }

      var full = await orderRepo.GetWithItemsAsync(created.Id);
      return Results.Created($"/api/orders/{created.Id}", full!.Adapt<OrderModel>());
    })
    .WithName("CreateOrder")
    .WithSummary("Place a new order at the POS — calculates VAT and awards loyalty points automatically");

    // PATCH /api/orders/{id}/status
    group.MapPatch("/{id:int}/status", async (int id, UpdateOrderStatusModel dto, IOrderRepository repo) =>
    {
      var ok = await repo.UpdateStatusAsync(id, dto.Status);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("UpdateOrderStatus");

    // PATCH /api/orders/{id}/payment
    group.MapPatch("/{id:int}/payment", async (int id, UpdatePaymentModel dto, IOrderRepository repo) =>
    {
      var ok = await repo.UpdatePaymentAsync(id, dto.PaymentStatus, dto.PaymentMethod);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("UpdateOrderPayment");

    group.MapDelete("/{id:int}", async (int id, IOrderRepository repo) =>
    {
      var deleted = await repo.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteOrder");
  }

}
