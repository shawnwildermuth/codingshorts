using BakeAndCake.Api.Repositories.Interfaces;
using BakeAndCake.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
  private readonly BakeAndCakeDbContext _db;
  public CustomerRepository(BakeAndCakeDbContext db) => _db = db;

  public async Task<IEnumerable<Customer>> GetAllAsync() =>
      await _db.Customers.AsNoTracking().OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();

  public async Task<Customer?> GetByIdAsync(int id) =>
      await _db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

  public async Task<Customer?> GetByEmailAsync(string email) =>
      await _db.Customers.AsNoTracking()
          .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());

  public async Task<IEnumerable<Customer>> SearchAsync(string term) =>
      await _db.Customers.AsNoTracking()
          .Where(c => c.FirstName.Contains(term)
                   || c.LastName.Contains(term)
                   || c.Email.Contains(term)
                   || (c.Phone != null && c.Phone.Contains(term)))
          .OrderBy(c => c.LastName)
          .ToListAsync();

  public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId) =>
      await _db.Orders.AsNoTracking()
          .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
          .Include(o => o.OrderItems).ThenInclude(oi => oi.CustomPie)
          .Where(o => o.CustomerId == customerId)
          .OrderByDescending(o => o.OrderDate)
          .ToListAsync();

  public async Task<bool> AdjustLoyaltyPointsAsync(int id, int points)
  {
    var customer = await _db.Customers.FindAsync(id);
    if (customer is null) return false;
    customer.LoyaltyPoints = Math.Max(0, customer.LoyaltyPoints + points);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<Customer> AddAsync(Customer entity)
  {
    _db.Customers.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<Customer> UpdateAsync(Customer entity)
  {
    _db.Customers.Update(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var customer = await _db.Customers.FindAsync(id);
    if (customer is null) return false;
    _db.Customers.Remove(customer);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id) =>
      await _db.Customers.AnyAsync(c => c.Id == id);
}
