namespace BakeAndCake.Api.Repositories.Interfaces;

public interface ICustomPieRepository : IRepository<CustomPie>
{
  Task<IEnumerable<CustomPie>> GetByCustomerAsync(int customerId);
  Task<IEnumerable<CustomPie>> GetPendingApprovalAsync();
  Task<CustomPie?> GetWithIngredientsAsync(int id);
  Task<bool> ApproveAsync(int id, decimal estimatedPrice);
}
