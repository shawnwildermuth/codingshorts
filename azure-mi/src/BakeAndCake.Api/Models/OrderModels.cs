namespace BakeAndCake.Api.Models;

public record OrderItemModel(
    int Id,
    int? ProductId,
    string? ProductName,
    int? CustomPieId,
    string? CustomPieName,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal,
    string? SpecialRequests
);

public record OrderModel(
    int Id,
    int? CustomerId,
    string? CustomerName,
    DateTime OrderDate,
    DateTime? RequiredByDate,
    OrderStatus Status,
    FulfilmentType Fulfilment,
    PaymentMethod PaymentMethod,
    PaymentStatus PaymentStatus,
    decimal SubTotal,
    decimal DiscountAmount,
    decimal TaxAmount,
    decimal TotalAmount,
    string? DeliveryAddress,
    string? Notes,
    string? ServedBy,
    IEnumerable<OrderItemModel> Items
);

public record CreateOrderItemModel(
    int? ProductId,
    int? CustomPieId,
    int Quantity,
    string? SpecialRequests
);

public record CreateOrderModel(
    int? CustomerId,
    DateTime? RequiredByDate,
    FulfilmentType Fulfilment,
    PaymentMethod PaymentMethod,
    decimal DiscountAmount,
    string? DeliveryAddress,
    string? Notes,
    string? ServedBy,
    IEnumerable<CreateOrderItemModel> Items
);

public record UpdateOrderStatusModel(OrderStatus Status);

public record UpdatePaymentModel(PaymentStatus PaymentStatus, PaymentMethod PaymentMethod);
