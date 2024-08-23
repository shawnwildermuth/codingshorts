using AddressBook.Api.Data;
using AddressBook.Api.Data.Entities;
using AddressBook.Api.Models;
using AddressBook.Apis;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AddressBook.Tests;

public class ApiTests
{
  private IServiceProvider Services = RegisterServices();

  static ServiceProvider RegisterServices()
  {
    return new ServiceCollection().AddTransient<IBookRepository>(_ =>
    {
      List<BookEntry> entries = new BookEntryFaker(new AddressFaker()).Generate(10);

      var mock = new Mock<IBookRepository>();
      mock.Setup(p => p.GetAllEntries())
        .Returns(() => Task.FromResult(entries.AsEnumerable()));

      mock.Setup(p => p.GetEntry(It.IsAny<int>()))
        .Returns((int id) => Task.FromResult(entries.Where(e => e.Id == id).FirstOrDefault()));

      mock.Setup(p => p.Add(It.IsAny<BookEntry>())).Callback<BookEntry>(entry =>
      {
        entry.Id = entries.Max(e => e.Id) + 1;
        entries.Add(entry);
      }).Verifiable();

      mock.Setup(p => p.SaveAllAsync())
        .Returns(() => Task.FromResult(true));

      mock.Setup(p => p.Remove(It.IsAny<BookEntry>()))
        .Callback<BookEntry>(entry =>
        {
          entries.Remove(entry);
        }).Verifiable();

      return mock.Object;
    }).BuildServiceProvider();
  }

  [Fact]
  public async Task CanGetAllEntries()
  {
    var repository = Services.GetRequiredService<IBookRepository>();

    var result = await EntriesApi.GetEntries(repository);

    Assert.IsAssignableFrom<Ok<IEnumerable<BookEntryModel>>>(result);

    var data = ((Ok<IEnumerable<BookEntryModel>>)result).Value;
    Assert.NotNull(data);
    Assert.NotEmpty(data);
  }

  [Fact]
  public async Task CanGetEntry()
  {
    var repository = Services.GetRequiredService<IBookRepository>();

    var result = await EntriesApi.GetEntries(repository);
    var data = ((Ok<IEnumerable<BookEntryModel>>)result).Value?.First();

    var entryResult = await EntriesApi.GetEntry(repository, data!.Id);

    Assert.IsAssignableFrom<Ok<BookEntryModel>>(entryResult);

    var entry = ((Ok<BookEntryModel>)entryResult).Value;
    Assert.NotNull(entry);
    Assert.Equivalent(data, entry);
  }

  [Fact]
  public async Task CanSaveEntry()
  {
    var repository = Services.GetRequiredService<IBookRepository>();

    var model = new BookEntryModel
    {
      CompanyName = "Foo Bar",
      FirstName = "Beth",
      LastName = "Smith",
      Email = "bsmith@aol.com",
      HomePhone = "404-555-1212"
    };
    var result = await EntriesApi.PostEntry(repository, model);
    var data = ((Created<BookEntryModel>)result).Value;

    Assert.NotNull(data);
    Assert.Equal("Beth", data.FirstName);
    Assert.Equal("bsmith@aol.com", data.Email);
    Assert.True(data.Id != default);

    var entryResult = await EntriesApi.GetEntry(repository, data.Id);

    Assert.IsAssignableFrom<Ok<BookEntryModel>>(entryResult);

    var entry = ((Ok<BookEntryModel>)entryResult).Value;
    Assert.NotNull(entry);
    Assert.Equivalent(data, entry);
  }

  [Fact]
  public async Task CanDeleteEntry()
  {
    var repository = Services.GetRequiredService<IBookRepository>();

    var model = new BookEntryModel
    {
      CompanyName = "Another Name",
      FirstName = "John",
      LastName = "Jenny",
      Email = "jjenny@aol.com",
      HomePhone = "404-555-1213"
    };
    var result = await EntriesApi.PostEntry(repository, model);
    var data = ((Created<BookEntryModel>)result).Value;

    Assert.NotNull(data);

    var deleteResult = await EntriesApi.DeleteEntry(repository, data.Id);
    Assert.IsAssignableFrom<Ok>(deleteResult);

    var entryResult = await EntriesApi.GetEntry(repository, data.Id);
    Assert.IsAssignableFrom<NotFound>(entryResult);
  }
}