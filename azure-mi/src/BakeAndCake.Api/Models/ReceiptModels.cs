namespace BakeAndCake.Api.Models;

public record ReceiptModel(
    int Id,
    int OrderId,
    string ReceiptNumber,
    DateTime IssuedAt,
    decimal AmountPaid,
    decimal ChangeGiven,
    decimal OrderTotal
);

public record CreateReceiptModel(int OrderId, decimal AmountPaid);
