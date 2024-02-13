using AddressBook.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Api.Data;

public class BookContext
  : DbContext
{
  private readonly IConfiguration _config;
  private readonly BookEntryFaker _entryFaker;
  private readonly AddressFaker _addrFaker;

  public BookContext(IConfiguration config, 
    DbContextOptions<BookContext> opt,
    BookEntryFaker entryFaker,
    AddressFaker addrFaker)
    :base(opt)
  {
    _config = config;
    _entryFaker = entryFaker;
    _addrFaker = addrFaker;
  }

  public DbSet<BookEntry> BookEntries => Set<BookEntry>();
  public DbSet<Address> Addresses => Set<Address>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);

    optionsBuilder.UseSqlServer(_config.GetConnectionString("AddressBookDb"));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    var entries = _entryFaker.Generate(25);
    var addresses = _addrFaker.Generate(50);

    int counter = 0;
    foreach (var entry in entries)
    {
      var entryAddresses = addresses.Skip(counter).Take(2);
      counter += 2;

      entryAddresses.ElementAt(0).BookEntryId = entry.Id;
      entryAddresses.ElementAt(1).BookEntryId = entry.Id;

    }

    modelBuilder.Entity<Address>()
      .HasData(addresses);

    modelBuilder.Entity<BookEntry>()
      .HasData(entries);
  }
}