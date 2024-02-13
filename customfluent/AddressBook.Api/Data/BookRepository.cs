using Microsoft.EntityFrameworkCore;

namespace AddressBook.Api.Data;

public class BookRepository(BookContext context) : IBookRepository
{

  public async Task<IEnumerable<BookEntry>> GetAllEntries()
  {
    return await context.BookEntries
      .Include(b => b.Addresses)
      //.Where(b => b.UserName == Thread.CurrentPrincipal?.Identity?.Name)
      .OrderBy(b => b.LastName)
      .ThenBy(b => b.FirstName)
      .ToListAsync();
  }

  public async Task<BookEntry?> GetEntry(int id)
  {
    return await context.BookEntries
      .Include(b => b.Addresses)
      .Where(b => b.Id == id)
      //.Where(b => b.UserName == Thread.CurrentPrincipal?.Identity?.Name)
      .FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<Address>> GetAddressesForUser(int id)
  {
    var results = await context.Addresses
      .Where(a => a.BookEntryId == id)
      .ToListAsync();

    if (!results.Any()) return new List<Address>();

    return results;
  }

  public async Task<Address?> GetAddressForUser(int id, int addressId)
  {
    var address = await context.Addresses
      .Where(a => a.BookEntryId == id && a.Id == addressId)
      .FirstOrDefaultAsync();

    if (address is null) return null;

    return address;
  }

  public void Add<T>(T entity) where T : class
  {
    context.Add(entity);
  }

  public void Remove<T>(T entity) where T : class
  {
    context.Remove(entity);
  }

  public async Task<bool> SaveAllAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<IEnumerable<BookEntry>> GetLookupEntries()
  {
    return await context.BookEntries
      //.Where(b => b.UserName == Thread.CurrentPrincipal?.Identity?.Name)
      .OrderBy(b => b.LastName)
      .ThenBy(b => b.FirstName)
      .Select(s => new BookEntry()
      {
        Id = s.Id,
        FirstName = s.FirstName,
        MiddleName = s.MiddleName,
        LastName = s.LastName,
        CompanyName = s.CompanyName
      })
      .ToListAsync();
  }
}
