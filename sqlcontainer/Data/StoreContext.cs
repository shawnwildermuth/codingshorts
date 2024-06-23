using System;
using SqlDev.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SqlDev.StoreFakers;
using Bogus;

namespace SqlDev.Data;

public class StoreContext(DbContextOptions options, Fakers fakers) 
  : DbContext(options)
{


  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Address> Addresses => Set<Address>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    var addresses = fakers.GetAddressGenerator().Generate(50);

    modelBuilder.Entity<Address>()
      .HasData(addresses);

    var customers = fakers.GetCustomerGenerator(false).Generate(50);

    for (var x = 0; x < customers.Count(); ++x)
    {
      customers[x].AddressId = addresses[x].Id;
    }

    modelBuilder.Entity<Customer>()
      .HasData(customers);

  }
}
