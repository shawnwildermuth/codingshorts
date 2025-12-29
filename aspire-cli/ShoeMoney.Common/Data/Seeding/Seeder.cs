using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace ShoeMoney.Data.Seeding;
public class Seeder(ShoeContext context)
{
  public void Seed()
  {
    if (!context.Products.Any())
    {
      var strategy = context.Database.CreateExecutionStrategy();
      context.Products.AddRange(SeedData.GetProducts());
      context.Categories.AddRange(SeedData.GetCategories());

      strategy.Execute(() =>
      {
        try
        {
          context.BulkSaveChanges();
        }
        catch (Exception ex)
        {
          var msg = ex.Message;
        }
      });
    }
  }
}
