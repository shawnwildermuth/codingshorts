namespace BakeAndCake.Api.Data.Entities;

public enum OrderStatus
{
  Pending,
  Confirmed,
  Baking,
  ReadyForCollection,
  OutForDelivery,
  Completed,
  Cancelled
}