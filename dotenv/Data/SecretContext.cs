using Microsoft.EntityFrameworkCore;

namespace PersonalSecrets.Data;

public class SecretContext : DbContext
{
  private readonly IConfiguration configuration;

  public SecretContext(IConfiguration configuration)
  {
    this.configuration = configuration;
  }

  public DbSet<Secret> Secrets => Set<Secret>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("SecretDb"));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Secret>()
      .HasData(new[]
      {
        new 
        {
          Id = 1,
          Contents = "I have a secret..."
        },
        new 
        {
          Id = 2,
          Contents = "My hair isn't blonde"
        },
      });
  }
}