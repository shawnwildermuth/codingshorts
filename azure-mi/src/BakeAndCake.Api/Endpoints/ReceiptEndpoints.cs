using Mapster;

namespace BakeAndCake.Api.Endpoints;

public static class ReceiptEndpoints
{
  public static void MapReceiptEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/api/receipts").WithTags("Receipts");

    group.MapGet("/", async (IReceiptRepository repo) =>
        Results.Ok((await repo.GetAllAsync()).Select(r => r.Adapt<ReceiptModel>())))
        .WithName("GetAllReceipts");

    group.MapGet("/{id:int}", async (int id, IReceiptRepository repo) =>
    {
      var receipt = await repo.GetByIdAsync(id);
      return receipt is null ? Results.NotFound() : Results.Ok(receipt.Adapt<ReceiptModel>());
    })
    .WithName("GetReceiptById");

    group.MapGet("/order/{orderId:int}", async (int orderId, IReceiptRepository repo) =>
    {
      var receipt = await repo.GetByOrderAsync(orderId);
      return receipt is null ? Results.NotFound() : Results.Ok(receipt.Adapt<ReceiptModel>());
    })
    .WithName("GetReceiptByOrder");

    group.MapGet("/number/{number}", async (string number, IReceiptRepository repo) =>
    {
      var receipt = await repo.GetByReceiptNumberAsync(number);
      return receipt is null ? Results.NotFound() : Results.Ok(receipt.Adapt<ReceiptModel>());
    })
    .WithName("GetReceiptByNumber");

    // POST /api/receipts — issue a receipt and mark the order as paid
    group.MapPost("/", async (
        CreateReceiptModel dto,
        IReceiptRepository receiptRepo,
        IOrderRepository orderRepo) =>
    {
      var order = await orderRepo.GetByIdAsync(dto.OrderId);
      if (order is null)
        return Results.NotFound(new { message = $"Order {dto.OrderId} not found." });

      if (await receiptRepo.GetByOrderAsync(dto.OrderId) is not null)
        return Results.Conflict(new ErrorModel { Message = "A receipt has already been issued for this order." });

      if (dto.AmountPaid < order.TotalAmount)
        return Results.BadRequest(new
        {
          message = $"Amount tendered (£{dto.AmountPaid:F2}) is less than the order total (£{order.TotalAmount:F2})."
        });

      var change = dto.AmountPaid - order.TotalAmount;

      var receipt = new Receipt
      {
        OrderId = dto.OrderId,
        ReceiptNumber = $"BPS-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}",
        AmountPaid = dto.AmountPaid,
        ChangeGiven = change
      };

      // Mark order paid
      await orderRepo.UpdatePaymentAsync(dto.OrderId, PaymentStatus.Paid, order.PaymentMethod);
      await orderRepo.UpdateStatusAsync(dto.OrderId, OrderStatus.Completed);

      var created = await receiptRepo.AddAsync(receipt);
      return Results.Created($"/api/receipts/{created.Id}",
              created.Adapt<ReceiptModel>() with { OrderTotal = order.TotalAmount });
    })
    .WithName("IssueReceipt")
    .WithSummary("Issue a receipt, mark order as Paid and Completed, calculate change");

    group.MapDelete("/{id:int}", async (int id, IReceiptRepository repo) =>
    {
      var deleted = await repo.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteReceipt");
  }

}
