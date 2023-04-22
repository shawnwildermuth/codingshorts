using RepoOrNot.Data.Entities;

namespace RepoOrNot.Data;

public interface IStoreRepository
{
  Task<IEnumerable<Order>> GetOrders();
  Task<IEnumerable<Order>> GetOrdersByProductName(string productName);
}