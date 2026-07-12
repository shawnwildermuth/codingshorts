using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace BakeAndCake.Api.Data;

public class BakeAndCakeDbContext : DbContext
{
  [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
  public BakeAndCakeDbContext(DbContextOptions<BakeAndCakeDbContext> options)
      : base(options) { }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Ingredient> Ingredients => Set<Ingredient>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<ProductIngredient> ProductIngredients => Set<ProductIngredient>();
  public DbSet<CustomPie> CustomPies => Set<CustomPie>();
  public DbSet<CustomPieIngredient> CustomPieIngredients => Set<CustomPieIngredient>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();
  public DbSet<Receipt> Receipts => Set<Receipt>();

  protected override void OnModelCreating(ModelBuilder mb)
  {
    mb.Entity<Customer>(e =>
    {
      e.ToTable("Customers");
      e.HasKey(c => c.Id);
      e.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
      e.Property(c => c.LastName).IsRequired().HasMaxLength(100);
      e.Property(c => c.Email).IsRequired().HasMaxLength(256);
      e.HasIndex(c => c.Email).IsUnique().HasDatabaseName("IX_Customers_Email");
      e.Property(c => c.Phone).HasMaxLength(30);
      e.Property(c => c.Address).HasMaxLength(500);
    });

    mb.Entity<Ingredient>(e =>
    {
      e.ToTable("Ingredients");
      e.HasKey(i => i.Id);
      e.Property(i => i.Name).IsRequired().HasMaxLength(200);
      e.Property(i => i.Unit).IsRequired().HasMaxLength(20);
      e.Property(i => i.CostPerUnit).HasColumnType("decimal(18,4)");
      e.Property(i => i.StockQuantity).HasColumnType("decimal(18,4)");
      e.Property(i => i.ReorderThreshold).HasColumnType("decimal(18,4)");
      e.Property(i => i.AllergenInfo).HasMaxLength(500);
    });

    mb.Entity<Product>(e =>
    {
      e.ToTable("Products");
      e.HasKey(p => p.Id);
      e.Property(p => p.Name).IsRequired().HasMaxLength(100);
      e.Property(p => p.Description).HasMaxLength(200);
      e.Property(p => p.ShortDescription).HasMaxLength(500);
      e.Property(p => p.Price).HasColumnType("decimal(18,2)");
      e.Property(p => p.Category).HasConversion<string>().HasMaxLength(50);
      e.Property(p => p.AllergyInformation).HasMaxLength(1000);
      e.Property(p => p.ImageUrl).HasMaxLength(500);
    });

    mb.Entity<ProductIngredient>(e =>
    {
      e.ToTable("ProductIngredients");
      e.HasKey(pi => new { pi.ProductId, pi.IngredientId });
      e.Property(pi => pi.QuantityRequired).HasColumnType("decimal(18,4)");
      e.HasOne(pi => pi.Product)
           .WithMany(p => p.ProductIngredients)
           .HasForeignKey(pi => pi.ProductId)
           .OnDelete(DeleteBehavior.Cascade);
      e.HasOne(pi => pi.Ingredient)
           .WithMany(i => i.ProductIngredients)
           .HasForeignKey(pi => pi.IngredientId)
           .OnDelete(DeleteBehavior.Restrict);
    });

    mb.Entity<CustomPie>(e =>
    {
      e.ToTable("CustomPies");
      e.HasKey(cp => cp.Id);
      e.Property(cp => cp.Name).IsRequired().HasMaxLength(200);
      e.Property(cp => cp.DedicationMessage).HasMaxLength(500);
      e.Property(cp => cp.SpecialInstructions).HasMaxLength(1000);
      e.Property(cp => cp.EstimatedPrice).HasColumnType("decimal(18,2)");
      e.Property(cp => cp.Size).HasConversion<string>().HasMaxLength(20);
      e.Property(cp => cp.CrustType).HasConversion<string>().HasMaxLength(30);
      e.Property(cp => cp.PrimaryFilling).HasConversion<string>().HasMaxLength(30);
      e.HasOne(cp => cp.Customer)
           .WithMany(c => c.CustomPies)
           .HasForeignKey(cp => cp.CustomerId)
           .OnDelete(DeleteBehavior.Restrict);
    });

    mb.Entity<CustomPieIngredient>(e =>
    {
      e.ToTable("CustomPieIngredients");
      e.HasKey(cpi => new { cpi.CustomPieId, cpi.IngredientId });
      e.Property(cpi => cpi.Quantity).HasColumnType("decimal(18,4)");
      e.HasOne(cpi => cpi.CustomPie)
           .WithMany(cp => cp.CustomPieIngredients)
           .HasForeignKey(cpi => cpi.CustomPieId)
           .OnDelete(DeleteBehavior.Cascade);
      e.HasOne(cpi => cpi.Ingredient)
           .WithMany(i => i.CustomPieIngredients)
           .HasForeignKey(cpi => cpi.IngredientId)
           .OnDelete(DeleteBehavior.Restrict);
    });

    mb.Entity<Order>(e =>
    {
      e.ToTable("Orders");
      e.HasKey(o => o.Id);
      e.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);
      e.Property(o => o.Fulfilment).HasConversion<string>().HasMaxLength(30);
      e.Property(o => o.PaymentMethod).HasConversion<string>().HasMaxLength(30);
      e.Property(o => o.PaymentStatus).HasConversion<string>().HasMaxLength(20);
      e.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
      e.Property(o => o.DiscountAmount).HasColumnType("decimal(18,2)");
      e.Property(o => o.TaxAmount).HasColumnType("decimal(18,2)");
      e.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
      e.Property(o => o.DeliveryAddress).HasMaxLength(500);
      e.Property(o => o.Notes).HasMaxLength(1000);
      e.Property(o => o.ServedBy).HasMaxLength(100);
      e.HasOne(o => o.Customer)
           .WithMany(c => c.Orders)
           .HasForeignKey(o => o.CustomerId)
           .OnDelete(DeleteBehavior.SetNull);
      e.HasIndex(o => o.OrderDate).HasDatabaseName("IX_Orders_OrderDate");
      e.HasIndex(o => o.Status).HasDatabaseName("IX_Orders_Status");
    });

    mb.Entity<OrderItem>(e =>
    {
      e.ToTable("OrderItems");
      e.HasKey(oi => oi.Id);
      e.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
      e.Property(oi => oi.LineTotal).HasColumnType("decimal(18,2)");
      e.Property(oi => oi.SpecialRequests).HasMaxLength(500);
      e.HasOne(oi => oi.Order)
           .WithMany(o => o.OrderItems)
           .HasForeignKey(oi => oi.OrderId)
           .OnDelete(DeleteBehavior.Cascade);
      e.HasOne(oi => oi.Product)
           .WithMany(p => p.OrderItems)
           .HasForeignKey(oi => oi.ProductId)
           .OnDelete(DeleteBehavior.SetNull);
      e.HasOne(oi => oi.CustomPie)
           .WithMany(cp => cp.OrderItems)
           .HasForeignKey(oi => oi.CustomPieId)
           .OnDelete(DeleteBehavior.SetNull);
    });

    mb.Entity<Receipt>(e =>
    {
      e.ToTable("Receipts");
      e.HasKey(r => r.Id);
      e.Property(r => r.ReceiptNumber).IsRequired().HasMaxLength(50);
      e.HasIndex(r => r.ReceiptNumber).IsUnique().HasDatabaseName("IX_Receipts_ReceiptNumber");
      e.Property(r => r.AmountPaid).HasColumnType("decimal(18,2)");
      e.Property(r => r.ChangeGiven).HasColumnType("decimal(18,2)");
      e.HasOne(r => r.Order)
           .WithOne(o => o.Receipt)
           .HasForeignKey<Receipt>(r => r.OrderId)
           .OnDelete(DeleteBehavior.Cascade);
    });

    // ═══════════════════════════════════════════════════════════════════════
    //  SEED DATA  –  Bake and Cake
    // ═══════════════════════════════════════════════════════════════════════

    mb.Entity<Ingredient>().HasData(
        new Ingredient { Id = 1, Name = "Plain Flour", Unit = "g", CostPerUnit = 0.0015m, StockQuantity = 20000, ReorderThreshold = 4000, IsAllergen = true, AllergenInfo = "Gluten" },
        new Ingredient { Id = 2, Name = "Butter", Unit = "g", CostPerUnit = 0.0120m, StockQuantity = 10000, ReorderThreshold = 2000, IsAllergen = true, AllergenInfo = "Dairy" },
        new Ingredient { Id = 3, Name = "Caster Sugar", Unit = "g", CostPerUnit = 0.0020m, StockQuantity = 15000, ReorderThreshold = 3000, IsAllergen = false },
        new Ingredient { Id = 4, Name = "Free-Range Eggs", Unit = "pc", CostPerUnit = 0.3500m, StockQuantity = 300, ReorderThreshold = 60, IsAllergen = true, AllergenInfo = "Eggs" },
        new Ingredient { Id = 5, Name = "Whole Milk", Unit = "ml", CostPerUnit = 0.0008m, StockQuantity = 15000, ReorderThreshold = 3000, IsAllergen = true, AllergenInfo = "Dairy" },
        new Ingredient { Id = 6, Name = "Double Cream", Unit = "ml", CostPerUnit = 0.0035m, StockQuantity = 5000, ReorderThreshold = 1000, IsAllergen = true, AllergenInfo = "Dairy" },
        new Ingredient { Id = 7, Name = "Bramley Apples", Unit = "g", CostPerUnit = 0.0040m, StockQuantity = 12000, ReorderThreshold = 2000, IsAllergen = false },
        new Ingredient { Id = 8, Name = "Blackberries", Unit = "g", CostPerUnit = 0.0120m, StockQuantity = 5000, ReorderThreshold = 1000, IsAllergen = false },
        new Ingredient { Id = 9, Name = "Cherry (pitted)", Unit = "g", CostPerUnit = 0.0090m, StockQuantity = 6000, ReorderThreshold = 1200, IsAllergen = false },
        new Ingredient { Id = 10, Name = "Cocoa Powder", Unit = "g", CostPerUnit = 0.0180m, StockQuantity = 3000, ReorderThreshold = 600, IsAllergen = false },
        new Ingredient { Id = 11, Name = "Dark Chocolate", Unit = "g", CostPerUnit = 0.0250m, StockQuantity = 4000, ReorderThreshold = 800, IsAllergen = true, AllergenInfo = "May contain traces of nuts" },
        new Ingredient { Id = 12, Name = "Minced Beef", Unit = "g", CostPerUnit = 0.0110m, StockQuantity = 8000, ReorderThreshold = 1500, IsAllergen = false },
        new Ingredient { Id = 13, Name = "Diced Chicken", Unit = "g", CostPerUnit = 0.0095m, StockQuantity = 8000, ReorderThreshold = 1500, IsAllergen = false },
        new Ingredient { Id = 14, Name = "Leeks", Unit = "g", CostPerUnit = 0.0030m, StockQuantity = 5000, ReorderThreshold = 1000, IsAllergen = false },
        new Ingredient { Id = 15, Name = "Cheddar Cheese", Unit = "g", CostPerUnit = 0.0140m, StockQuantity = 4000, ReorderThreshold = 800, IsAllergen = true, AllergenInfo = "Dairy" },
        new Ingredient { Id = 16, Name = "Icing Sugar", Unit = "g", CostPerUnit = 0.0025m, StockQuantity = 5000, ReorderThreshold = 1000, IsAllergen = false },
        new Ingredient { Id = 17, Name = "Vanilla Extract", Unit = "ml", CostPerUnit = 0.0500m, StockQuantity = 500, ReorderThreshold = 100, IsAllergen = false },
        new Ingredient { Id = 18, Name = "Baking Powder", Unit = "g", CostPerUnit = 0.0050m, StockQuantity = 2000, ReorderThreshold = 400, IsAllergen = false },
        new Ingredient { Id = 19, Name = "Salt", Unit = "g", CostPerUnit = 0.0005m, StockQuantity = 5000, ReorderThreshold = 500, IsAllergen = false },
        new Ingredient { Id = 20, Name = "Puff Pastry Sheets", Unit = "pc", CostPerUnit = 1.2000m, StockQuantity = 100, ReorderThreshold = 20, IsAllergen = true, AllergenInfo = "Gluten, Dairy" }
    );

    mb.Entity<Product>().HasData(
        new Product { Id = 1, Name = "Classic Apple Pie", ShortDescription = "Old family recipe, Bramley apples & cinnamon", Price = 12.95m, Category = ProductCategory.Pie, PreparationTimeMinutes = 60, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy, Eggs" },
        new Product { Id = 2, Name = "Cherry & Almond Tart", ShortDescription = "Sweet shortcrust with fresh cherries", Price = 8.50m, Category = ProductCategory.Tart, PreparationTimeMinutes = 45, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy, Eggs, Nuts" },
        new Product { Id = 3, Name = "Steak & Ale Pie", ShortDescription = "Slow-braised beef in rich ale gravy", Price = 14.50m, Category = ProductCategory.Pie, PreparationTimeMinutes = 90, IsAvailable = true, IsPieOfTheWeek = true, AllergyInformation = "Contains Gluten, Dairy" },
        new Product { Id = 4, Name = "Chicken, Leek & Cheese Pie", ShortDescription = "Creamy filling with Cheddar top crust", Price = 13.00m, Category = ProductCategory.Pie, PreparationTimeMinutes = 75, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy, Eggs" },
        new Product { Id = 5, Name = "Chocolate Silk Tart", ShortDescription = "Dark chocolate ganache in a crisp shell", Price = 9.95m, Category = ProductCategory.Tart, PreparationTimeMinutes = 50, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy, Eggs" },
        new Product { Id = 6, Name = "Blackberry & Apple Crumble Pie", ShortDescription = "Seasonal berries under a buttery crumble", Price = 11.50m, Category = ProductCategory.Pie, PreparationTimeMinutes = 55, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy" },
        new Product { Id = 7, Name = "Cheese & Leek Quiche", ShortDescription = "Savory custard with Cheddar & fresh leeks", Price = 8.00m, Category = ProductCategory.Quiche, PreparationTimeMinutes = 50, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy, Eggs" },
        new Product { Id = 8, Name = "Vanilla Custard Tart", ShortDescription = "Silky-smooth custard in shortcrust pastry", Price = 7.50m, Category = ProductCategory.Tart, PreparationTimeMinutes = 40, IsAvailable = true, IsPieOfTheWeek = false, AllergyInformation = "Contains Gluten, Dairy, Eggs" }
    );

    // Link pies to their main ingredients
    mb.Entity<ProductIngredient>().HasData(
        // Classic Apple Pie
        new ProductIngredient { ProductId = 1, IngredientId = 1, QuantityRequired = 350 },  // flour
        new ProductIngredient { ProductId = 1, IngredientId = 2, QuantityRequired = 175 },  // butter
        new ProductIngredient { ProductId = 1, IngredientId = 3, QuantityRequired = 120 },  // sugar
        new ProductIngredient { ProductId = 1, IngredientId = 4, QuantityRequired = 2 },  // eggs
        new ProductIngredient { ProductId = 1, IngredientId = 7, QuantityRequired = 750 },  // apples
                                                                                            // Steak & Ale Pie
        new ProductIngredient { ProductId = 3, IngredientId = 1, QuantityRequired = 300 },
        new ProductIngredient { ProductId = 3, IngredientId = 2, QuantityRequired = 150 },
        new ProductIngredient { ProductId = 3, IngredientId = 12, QuantityRequired = 600 },  // beef
                                                                                             // Chicken, Leek & Cheese Pie
        new ProductIngredient { ProductId = 4, IngredientId = 1, QuantityRequired = 300 },
        new ProductIngredient { ProductId = 4, IngredientId = 2, QuantityRequired = 150 },
        new ProductIngredient { ProductId = 4, IngredientId = 13, QuantityRequired = 500 },  // chicken
        new ProductIngredient { ProductId = 4, IngredientId = 14, QuantityRequired = 200 },  // leeks
        new ProductIngredient { ProductId = 4, IngredientId = 15, QuantityRequired = 150 },  // cheese
                                                                                             // Chocolate Silk Tart
        new ProductIngredient { ProductId = 5, IngredientId = 1, QuantityRequired = 200 },
        new ProductIngredient { ProductId = 5, IngredientId = 2, QuantityRequired = 100 },
        new ProductIngredient { ProductId = 5, IngredientId = 11, QuantityRequired = 300 },  // dark choc
        new ProductIngredient { ProductId = 5, IngredientId = 6, QuantityRequired = 250 }   // double cream
    );

    mb.Entity<Customer>().HasData(
        new Customer { Id = 1, FirstName = "Margaret", LastName = "Whitfield", Email = "margaret.whitfield@email.com", Phone = "07700900001", IsLoyaltyMember = true, LoyaltyPoints = 340, CreatedAt = new DateTime(2023, 3, 1, 0, 0, 0, DateTimeKind.Utc) },
        new Customer { Id = 2, FirstName = "Robert", LastName = "Craine", Email = "robert.craine@email.com", Phone = "07700900002", IsLoyaltyMember = true, LoyaltyPoints = 120, CreatedAt = new DateTime(2023, 6, 15, 0, 0, 0, DateTimeKind.Utc) },
        new Customer { Id = 3, FirstName = "Saoirse", LastName = "Murphy", Email = "saoirse.murphy@email.com", Phone = null, IsLoyaltyMember = false, LoyaltyPoints = 0, CreatedAt = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc) }
    );
  }
}
