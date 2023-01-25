using System;
using FakingIt.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FakingIt.Services;

public class FakingContext : DbContext
{

  ILogger<FakingContext> _logger;
  private readonly Fakers _fakers;

  public FakingContext(DbContextOptions options,
    ILogger<FakingContext> logger,
    Fakers fakers)
    : base(options)
  {
    _logger = logger;
    _fakers = fakers;
  }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Address> Addresses => Set<Address>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    var addresses = _fakers.GetAddressGenerator().Generate(50);

    modelBuilder.Entity<Address>()
      .HasData(addresses);

    var customers = _fakers.GetCustomerGenerator(false).Generate(50);

    for (var x = 0; x < customers.Count(); ++x)
    {
      customers[x].AddressId = addresses[x].Id;
    }

    modelBuilder.Entity<Customer>()
      .HasData(customers);

  }
}
