namespace BakeAndCake.Api.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
  Task<IEnumerable<Product>> GetAvailableAsync();
  Task<IEnumerable<Product>> GetByCategoryAsync(ProductCategory category);
  Task<Product?> GetWithIngredientsAsync(int id);
  Task<Product?> GetPieOfTheWeekAsync();
  Task<bool> SetAvailabilityAsync(int id, bool available);
  Task<bool> SetPieOfTheWeekAsync(int id);
}
