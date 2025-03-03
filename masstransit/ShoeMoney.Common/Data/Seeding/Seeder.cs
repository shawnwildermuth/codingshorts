using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;

namespace ShoeMoney.Data.Seeding;
public class Seeder(ShoeContext context)
{
  public void Seed()
  {
    if (!context.Products.Any())
    {
      var strategy = context.Database.CreateExecutionStrategy();
      context.Categories.AddRange(SeedData.GetCategories());
      context.Products.AddRange(SeedData.GetProducts());

      strategy.Execute(() =>
      {
        context.BulkSaveChanges();
      });
    }
  }
}
