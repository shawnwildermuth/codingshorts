using BakeAndCake.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Repositories;

public class IngredientRepository : IIngredientRepository
{
  private readonly BakeAndCakeDbContext _db;
  public IngredientRepository(BakeAndCakeDbContext db) => _db = db;

  public async Task<IEnumerable<Ingredient>> GetAllAsync() =>
      await _db.Ingredients.AsNoTracking().OrderBy(i => i.Name).ToListAsync();

  public async Task<Ingredient?> GetByIdAsync(int id) =>
      await _db.Ingredients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

  public async Task<IEnumerable<Ingredient>> GetLowStockAsync() =>
      await _db.Ingredients.AsNoTracking()
          .Where(i => i.StockQuantity <= i.ReorderThreshold)
          .OrderBy(i => i.StockQuantity)
          .ToListAsync();

  public async Task<IEnumerable<Ingredient>> GetAllergensAsync() =>
      await _db.Ingredients.AsNoTracking()
          .Where(i => i.IsAllergen)
          .OrderBy(i => i.Name)
          .ToListAsync();

  public async Task<bool> AdjustStockAsync(int id, decimal quantity)
  {
    var ingredient = await _db.Ingredients.FindAsync(id);
    if (ingredient is null) return false;
    ingredient.StockQuantity = Math.Max(0, ingredient.StockQuantity + quantity);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<Ingredient> AddAsync(Ingredient entity)
  {
    _db.Ingredients.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<Ingredient> UpdateAsync(Ingredient entity)
  {
    _db.Ingredients.Update(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var ingredient = await _db.Ingredients.FindAsync(id);
    if (ingredient is null) return false;
    _db.Ingredients.Remove(ingredient);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id) =>
      await _db.Ingredients.AnyAsync(i => i.Id == id);
}
