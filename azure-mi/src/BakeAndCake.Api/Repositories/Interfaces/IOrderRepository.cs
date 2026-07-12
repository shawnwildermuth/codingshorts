namespace BakeAndCake.Api.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
  Task<Order?> GetWithItemsAsync(int id);
  Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
  Task<IEnumerable<Order>> GetByCustomerAsync(int customerId);
  Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime from, DateTime to);
  Task<bool> UpdateStatusAsync(int id, OrderStatus status);
  Task<bool> UpdatePaymentAsync(int id, PaymentStatus paymentStatus, PaymentMethod method);
  Task<decimal> GetDailyRevenueAsync(DateTime date);
}
