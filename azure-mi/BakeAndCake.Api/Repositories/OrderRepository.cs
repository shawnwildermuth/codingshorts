using BakeAndCake.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Repositories;

public class OrderRepository : IOrderRepository
{
  private readonly BakeAndCakeDbContext _db;
  public OrderRepository(BakeAndCakeDbContext db) => _db = db;

  private IQueryable<Order> WithIncludes() =>
      _db.Orders
         .Include(o => o.Customer)
         .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
         .Include(o => o.OrderItems).ThenInclude(oi => oi.CustomPie);

  public async Task<IEnumerable<Order>> GetAllAsync() =>
      await WithIncludes().AsNoTracking().OrderByDescending(o => o.OrderDate).ToListAsync();

  public async Task<Order?> GetByIdAsync(int id) =>
      await WithIncludes().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

  public async Task<Order?> GetWithItemsAsync(int id) => await GetByIdAsync(id);

  public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status) =>
      await WithIncludes().AsNoTracking()
          .Where(o => o.Status == status)
          .OrderBy(o => o.RequiredByDate ?? o.OrderDate)
          .ToListAsync();

  public async Task<IEnumerable<Order>> GetByCustomerAsync(int customerId) =>
      await WithIncludes().AsNoTracking()
          .Where(o => o.CustomerId == customerId)
          .OrderByDescending(o => o.OrderDate)
          .ToListAsync();

  public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime from, DateTime to) =>
      await WithIncludes().AsNoTracking()
          .Where(o => o.OrderDate >= from && o.OrderDate <= to)
          .OrderByDescending(o => o.OrderDate)
          .ToListAsync();

  public async Task<bool> UpdateStatusAsync(int id, OrderStatus status)
  {
    var order = await _db.Orders.FindAsync(id);
    if (order is null) return false;
    order.Status = status;
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> UpdatePaymentAsync(int id, PaymentStatus paymentStatus, PaymentMethod method)
  {
    var order = await _db.Orders.FindAsync(id);
    if (order is null) return false;
    order.PaymentStatus = paymentStatus;
    order.PaymentMethod = method;
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<decimal> GetDailyRevenueAsync(DateTime date)
  {
    var start = date.Date;
    var end = start.AddDays(1);
    return await _db.Orders
        .Where(o => o.OrderDate >= start
                 && o.OrderDate < end
                 && o.PaymentStatus == PaymentStatus.Paid)
        .SumAsync(o => (decimal?)o.TotalAmount) ?? 0m;
  }

  public async Task<Order> AddAsync(Order entity)
  {
    _db.Orders.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<Order> UpdateAsync(Order entity)
  {
    _db.Orders.Update(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var order = await _db.Orders.FindAsync(id);
    if (order is null) return false;
    _db.Orders.Remove(order);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id) =>
      await _db.Orders.AnyAsync(o => o.Id == id);
}
