using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ShoeMoney.Data.Entities;

namespace ShoeMoney.Data;

public  class ShoeContext : DbContext
{
  public ShoeContext(DbContextOptions<ShoeContext> options) : base(options)
  {
  }

  public DbSet<Order> Orders => Set<Order>();
  public DbSet<Category> OrderItems => Set<Category>();
  public DbSet<Address> Addresses => Set<Address>();
  public DbSet<Payment> Payments => Set<Payment>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<Category> Categories => Set<Category>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    base.OnModelCreating(bldr);

    bldr.Entity<Product>()
      .Property(p => p.Price).HasColumnType("decimal(6,2)");

    var orderItemEntity = bldr.Entity<OrderItem>();
    orderItemEntity.Property(i => i.Price).HasColumnType("decimal(6,2)");
    orderItemEntity.Property(i => i.Quantity).HasColumnType("decimal(6,2)");

    // Delete Order Items when Order is deleted
    orderItemEntity.HasOne<Order>()
      .WithMany(o => o.Items)
      .OnDelete(DeleteBehavior.ClientCascade);

    var orderEntity = bldr.Entity<Order>();
    orderEntity
      .Property(i => i.OrderStatus)
      .HasConversion<int>();

    orderEntity
      .Property(i => i.OrderType)
      .HasConversion<int>();

    bldr.Entity<Address>()
      .HasOne<Order>()
      .WithOne(o => o.ShippingAddress)
      .OnDelete(DeleteBehavior.ClientCascade);

    var paymentEntity = bldr.Entity<Payment>();
    paymentEntity.Property(p => p.Amount).HasColumnType("decimal(8,2)");
    paymentEntity.Property(p => p.PaymentType).HasConversion<int>();
    paymentEntity.HasOne<Order>()
      .WithOne(o => o.Payment)
      .OnDelete(DeleteBehavior.ClientCascade);

  }


}
