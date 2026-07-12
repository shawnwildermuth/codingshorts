using BakeAndCake.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Repositories;

public class ProductRepository : IProductRepository
{
  private readonly BakeAndCakeDbContext _db;
  public ProductRepository(BakeAndCakeDbContext db) => _db = db;

  private IQueryable<Product> WithIngredients() =>
      _db.Products
         .Include(p => p.ProductIngredients)
             .ThenInclude(pi => pi.Ingredient);

  public async Task<IEnumerable<Product>> GetAllAsync() =>
      await WithIngredients().AsNoTracking().OrderBy(p => p.Name).ToListAsync();

  public async Task<Product?> GetByIdAsync(int id) =>
      await WithIngredients().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

  public async Task<Product?> GetWithIngredientsAsync(int id) => await GetByIdAsync(id);

  public async Task<IEnumerable<Product>> GetAvailableAsync() =>
      await WithIngredients().AsNoTracking()
          .Where(p => p.IsAvailable)
          .OrderBy(p => p.Category).ThenBy(p => p.Name)
          .ToListAsync();

  public async Task<IEnumerable<Product>> GetByCategoryAsync(ProductCategory category) =>
      await WithIngredients().AsNoTracking()
          .Where(p => p.Category == category)
          .OrderBy(p => p.Name)
          .ToListAsync();

  public async Task<Product?> GetPieOfTheWeekAsync() =>
      await WithIngredients().AsNoTracking()
          .FirstOrDefaultAsync(p => p.IsPieOfTheWeek && p.IsAvailable);

  public async Task<bool> SetAvailabilityAsync(int id, bool available)
  {
    var product = await _db.Products.FindAsync(id);
    if (product is null) return false;
    product.IsAvailable = available;
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> SetPieOfTheWeekAsync(int id)
  {
    // Clear existing Pie of the Week, then set the new one
    await _db.Products
        .Where(p => p.IsPieOfTheWeek)
        .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsPieOfTheWeek, false));

    var product = await _db.Products.FindAsync(id);
    if (product is null) return false;
    product.IsPieOfTheWeek = true;
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<Product> AddAsync(Product entity)
  {
    _db.Products.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<Product> UpdateAsync(Product entity)
  {
    // Replace ingredient links atomically
    var existingLinks = await _db.ProductIngredients
        .Where(pi => pi.ProductId == entity.Id)
        .ToListAsync();
    _db.ProductIngredients.RemoveRange(existingLinks);
    _db.Products.Update(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var product = await _db.Products.FindAsync(id);
    if (product is null) return false;
    _db.Products.Remove(product);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id) =>
      await _db.Products.AnyAsync(p => p.Id == id);
}
