using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ShoeMoney.Data.Seeding;

public class Seeder(ShoeContext context)
{
  public void Seed()
  {

    if (!context.Products.AsNoTracking().Any())
    {
      try
      {
        context.Database.OpenConnection();

        context.Categories.AddRange(SeedData.GetCategories());
        context.Database.ExecuteSql($"SET IDENTITY_INSERT Categories ON");
        context.SaveChanges();
        context.Database.ExecuteSql($"SET IDENTITY_INSERT Categories OFF");

        context.Products.AddRange(SeedData.GetProducts());
        context.Database.ExecuteSql($"SET IDENTITY_INSERT Products ON");
        context.SaveChanges();
        context.Database.ExecuteSql($"SET IDENTITY_INSERT Products OFF");

      }
      finally
      {
        context.Database.CloseConnection();
      }

    }
  }
}
