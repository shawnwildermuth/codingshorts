using System.Linq.Expressions;

namespace RepoOrNot.Data
{
  public class GenericRepository
  {
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
      _context = context;
    }

    public IQueryable<T> Get<T>()
      where T : class
    {
        return _context.Set<T>();
    }

    public void Add<T>(T entity)
      where T : class
    {
      _context.Set<T>().Add(entity);
    }

    public void Remove<T>(T entity)
      where T : class
    {
      _context.Set<T>().Remove(entity);
    }

    public void Update<T>(T entity)
      where T : class
    {
      _context.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
      _context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }
  }
}
