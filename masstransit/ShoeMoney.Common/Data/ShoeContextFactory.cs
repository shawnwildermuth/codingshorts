using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShoeMoney.Data;
public class ShoeContextFactory : IDesignTimeDbContextFactory<ShoeContext>
{
  public ShoeContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ShoeContext>();

    // Hard coding dev server since this is only used to create migrations
    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDb;Database=ShoeMoneyDb;Integrated Security=true;");

    return new ShoeContext(optionsBuilder.Options);
  }

}
