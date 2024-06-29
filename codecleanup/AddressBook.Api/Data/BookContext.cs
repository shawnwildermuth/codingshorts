using AddressBook.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Api.Data;

public class BookContext
  : DbContext
{
  private readonly IConfiguration config;
  private readonly BookEntryFaker entryFaker;
  private readonly AddressFaker addrFaker;

  public BookContext(IConfiguration config, 
    DbContextOptions<BookContext> opt,
    BookEntryFaker entryFaker,
    AddressFaker addrFaker)
    :base(opt)
  {
    this.config = config;
    this.entryFaker = entryFaker;
    this.addrFaker = addrFaker;
  }

  public DbSet<BookEntry> BookEntries => Set<BookEntry>();
  public DbSet<Address> Addresses => Set<Address>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);

    optionsBuilder.UseSqlServer(config.GetConnectionString("AddressBookDb"));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    var entries = entryFaker.Generate(100);
    var addresses = new List<Address>();

    int counter = 0;
    foreach (var entry in entries)
    {
      var noOfAddresses = Random.Shared.Next(0, 2);
      var entryAddresses = addrFaker.Generate(noOfAddresses);
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