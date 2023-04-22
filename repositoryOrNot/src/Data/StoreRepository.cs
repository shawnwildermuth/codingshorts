using System.Collections;
using RepoOrNot.Data.Entities;

namespace RepoOrNot.Data;

public class StoreRepository : IStoreRepository
{
  private readonly StoreContext _context;

  public StoreRepository(StoreContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Order>> GetOrders()
  {
    return await _context.Orders
                 .Include(o => o.OrderItems)
                 .OrderBy(o => o.OrderDate)
                 .ToListAsync();
  }

  public async Task<IEnumerable<Order>> GetOrdersByProductName(string productName)
  {
    return await _context.Orders
                 .Include(o => o.OrderItems)
                 .OrderBy(o => o.OrderDate)
                 .Where(o => o.OrderItems.Count(i => i.ProductName.ToLower() == productName.ToLower()) > 0)
                 .ToListAsync();
  }
}
