namespace BakeAndCake.Api.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
  Task<Customer?> GetByEmailAsync(string email);
  Task<IEnumerable<Customer>> SearchAsync(string term);
  Task<bool> AdjustLoyaltyPointsAsync(int id, int points);
  Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
}
