using AddressBook.Api.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace AddressBook.Tests;
public class DatabaseFixture : IAsyncLifetime
{
  public IServiceProvider Services;
  private MsSqlContainer _container;

  public DatabaseFixture()
  {
    _container = new MsSqlBuilder()
      .Build();

    var coll = new ServiceCollection();

    coll.AddTransient<IConfiguration>(_ =>
    {
      var configs = new List<KeyValuePair<string, string?>>
      {
        new ("ConnectionStrings:AddressBookDb", _container.GetConnectionString())
      };

      return new ConfigurationBuilder()
      .AddInMemoryCollection(configs)
      .Build();
    });

    coll.AddDbContext<BookContext>();
    coll.AddTransient<IBookRepository, BookRepository>();
    coll.AddTransient<BookEntryFaker>();
    coll.AddTransient<AddressFaker>();

    Services = coll.BuildServiceProvider();

  }
  public async Task InitializeAsync()
  {
    await _container.StartAsync();
    var context = Services.GetRequiredService<BookContext>();
    await context.Database.EnsureCreatedAsync();
  }

  public async Task DisposeAsync()
  {
    await _container.StopAsync();
  }

}
