using BakeAndCake.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Repositories;

public class CustomPieRepository : ICustomPieRepository
{
  private readonly BakeAndCakeDbContext _db;
  public CustomPieRepository(BakeAndCakeDbContext db) => _db = db;

  private IQueryable<CustomPie> WithIncludes() =>
      _db.CustomPies
         .Include(cp => cp.Customer)
         .Include(cp => cp.CustomPieIngredients)
             .ThenInclude(cpi => cpi.Ingredient);

  public async Task<IEnumerable<CustomPie>> GetAllAsync() =>
      await WithIncludes().AsNoTracking().OrderByDescending(cp => cp.CreatedAt).ToListAsync();

  public async Task<CustomPie?> GetByIdAsync(int id) =>
      await WithIncludes().AsNoTracking().FirstOrDefaultAsync(cp => cp.Id == id);

  public async Task<CustomPie?> GetWithIngredientsAsync(int id) => await GetByIdAsync(id);

  public async Task<IEnumerable<CustomPie>> GetByCustomerAsync(int customerId) =>
      await WithIncludes().AsNoTracking()
          .Where(cp => cp.CustomerId == customerId)
          .OrderByDescending(cp => cp.CreatedAt)
          .ToListAsync();

  public async Task<IEnumerable<CustomPie>> GetPendingApprovalAsync() =>
      await WithIncludes().AsNoTracking()
          .Where(cp => !cp.IsApproved)
          .OrderBy(cp => cp.RequiredByDate ?? DateTime.MaxValue)
          .ToListAsync();

  public async Task<bool> ApproveAsync(int id, decimal estimatedPrice)
  {
    var pie = await _db.CustomPies.FindAsync(id);
    if (pie is null) return false;
    pie.IsApproved = true;
    pie.EstimatedPrice = estimatedPrice;
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<CustomPie> AddAsync(CustomPie entity)
  {
    _db.CustomPies.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<CustomPie> UpdateAsync(CustomPie entity)
  {
    var existingLinks = await _db.CustomPieIngredients
        .Where(cpi => cpi.CustomPieId == entity.Id)
        .ToListAsync();
    _db.CustomPieIngredients.RemoveRange(existingLinks);
    _db.CustomPies.Update(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var pie = await _db.CustomPies.FindAsync(id);
    if (pie is null) return false;
    _db.CustomPies.Remove(pie);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id) =>
      await _db.CustomPies.AnyAsync(cp => cp.Id == id);
}
