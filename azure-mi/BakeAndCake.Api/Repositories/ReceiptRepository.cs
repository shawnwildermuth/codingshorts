using BakeAndCake.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Repositories;

public class ReceiptRepository : IReceiptRepository
{
  private readonly BakeAndCakeDbContext _db;
  public ReceiptRepository(BakeAndCakeDbContext db) => _db = db;

  public async Task<IEnumerable<Receipt>> GetAllAsync() =>
      await _db.Receipts.AsNoTracking()
          .Include(r => r.Order)
          .OrderByDescending(r => r.IssuedAt)
          .ToListAsync();

  public async Task<Receipt?> GetByIdAsync(int id) =>
      await _db.Receipts.AsNoTracking()
          .Include(r => r.Order)
          .FirstOrDefaultAsync(r => r.Id == id);

  public async Task<Receipt?> GetByOrderAsync(int orderId) =>
      await _db.Receipts.AsNoTracking().FirstOrDefaultAsync(r => r.OrderId == orderId);

  public async Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber) =>
      await _db.Receipts.AsNoTracking().FirstOrDefaultAsync(r => r.ReceiptNumber == receiptNumber);

  public async Task<Receipt> AddAsync(Receipt entity)
  {
    _db.Receipts.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<Receipt> UpdateAsync(Receipt entity)
  {
    _db.Receipts.Update(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var receipt = await _db.Receipts.FindAsync(id);
    if (receipt is null) return false;
    _db.Receipts.Remove(receipt);
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id) =>
      await _db.Receipts.AnyAsync(r => r.Id == id);
}
