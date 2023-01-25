using System;
using FakingIt.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FakingIt.Services;

public class FakingContext : DbContext
{

  ILogger<FakingContext> _logger;

  public FakingContext(DbContextOptions options,
    ILogger<FakingContext> logger)
    : base(options)
  {
    _logger = logger;
    Database.EnsureCreated();
  }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Address> Addresses => Set<Address>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


  }
}
