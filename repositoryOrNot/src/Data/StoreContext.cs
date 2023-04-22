using Microsoft.EntityFrameworkCore;
using RepoOrNot.Data.Entities;

namespace RepoOrNot.Data;

public class StoreContext : DbContext
{
  private readonly IConfiguration _config;

  public StoreContext(IConfiguration config)
  {
    _config = config;
  }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();
  public DbSet<Product> Products => Set<Product>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
  {
    builder.UseSqlServer(_config.GetConnectionString("StoreDb"));
  }

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    // Do Default
    base.OnModelCreating(bldr);

    bldr.Entity<Customer>()
      .HasData(new[]
      {
        new
        {
          Id = 1,
          CompanyName= "Acme Shipping",
          Phone = "404-555-2020",
          ContactName = "Bob Smith"
        },
        new
        {
          Id = 2,
          CompanyName= "Pete's Coffee",
          Phone = "203-456-0098",
          ContactName = "Pete Johnson"
        }
      });

    bldr.Entity<Order>()
      .HasData(new[]
      {
        new
        {
          Id = 1,
          OrderDate = DateTime.UtcNow,
          DueDate = DateTime.UtcNow.AddDays(30),
          OrderNumber = "1000",
          CustomerId = 1,
          Terms = "Net 30"
        },
        new
        {
          Id = 2,
          OrderDate = DateTime.UtcNow,
          DueDate = DateTime.UtcNow.AddDays(30),
          OrderNumber = "1001",
          CustomerId = 1,
          Terms = "Net 90"
        },
        new
        {
          Id = 3,
          OrderDate = DateTime.UtcNow,
          DueDate = DateTime.UtcNow.AddDays(30),
          OrderNumber = "1002",
          CustomerId = 2,
          Terms = "Net 15"
        },
      });

    bldr.Entity<OrderItem>()
      .HasData(new[]
      {
        new
        {
          Id = 1,
          OrderId = 1,
          ProductName = "Labels",
          Quantity = 24d,
          UnitPrice = 14.99d,
          Discount = 0d
        },
        new
        {
          Id = 2,
          OrderId = 1,
          ProductName = "Boxes",
          Quantity = 144d,
          UnitPrice = 3.99d,
          Discount = .25d
        },
        new
        {
          Id = 3,
          OrderId = 1,
          ProductName = "Receipt Paper",
          Quantity = 4d,
          UnitPrice = 19.99d,
          Discount = 0d
        },
        new
        {
          Id = 4,
          OrderId = 2,
          ProductName = "Printer Ink",
          Quantity = 4d,
          UnitPrice = 114.98d,
          Discount = 0.05d
        },
        new
        {
          Id = 5,
          OrderId = 2,
          ProductName = "Boxes",
          Quantity = 24d,
          UnitPrice = 3.99d,
          Discount = .05d
        },
        new
        {
          Id = 6,
          OrderId = 3,
          ProductName = "Pens",
          Quantity = 12d,
          UnitPrice = 4.98d,
          Discount = 0d
        },
        new
        {
          Id = 7,
          OrderId = 3,
          ProductName = "Pencils",
          Quantity = 12d,
          UnitPrice = 1.99d,
          Discount = .1d
        },
      });

    bldr.Entity<Product>()
      .HasData(new[] 
      { 
        new Product()
        {
          Id = 1,
          ProductName = "Envelopes",
          UnitPrice = 4.99m,
          Unit = "Box"
        },
        new Product()
        {
          Id = 2,
          ProductName = "Binders",
          UnitPrice = 3.99m,
          Unit = "Each"
        }
      });

  }

}
