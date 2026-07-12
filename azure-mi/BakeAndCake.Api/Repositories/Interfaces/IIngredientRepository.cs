namespace BakeAndCake.Api.Repositories.Interfaces;

public interface IIngredientRepository : IRepository<Ingredient>
{
  Task<IEnumerable<Ingredient>> GetLowStockAsync();
  Task<IEnumerable<Ingredient>> GetAllergensAsync();
  Task<bool> AdjustStockAsync(int id, decimal quantity);
}
