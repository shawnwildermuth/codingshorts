namespace BakeAndCake.Api.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  Task<T> AddAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task<bool> DeleteAsync(int id);
  Task<bool> ExistsAsync(int id);
}
