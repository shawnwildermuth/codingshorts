namespace BakeAndCake.Api.Repositories.Interfaces;

public interface IReceiptRepository : IRepository<Receipt>
{
  Task<Receipt?> GetByOrderAsync(int orderId);
  Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber);
}
