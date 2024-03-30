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

    var entries = _entryFaker.Generate(100);
    var addresses = new List<Address>();

    int counter = 0;
    foreach (var entry in entries)
    {
      var noOfAddresses = Random.Shared.Next(0, 2);
      var entryAddresses = _addrFaker.Generate(noOfAddresses);
      addresses.AddRange(entryAddresses);
      counter += noOfAddresses;

      for (var x = 0; x < noOfAddresses; ++x)
      {
        entryAddresses.ElementAt(x).BookEntryId = entry.Id;
      }
    }

    modelBuilder.Entity<Address>()
      .HasData(addresses);

    modelBuilder.Entity<BookEntry>()
      .HasData(entries);
  }
}