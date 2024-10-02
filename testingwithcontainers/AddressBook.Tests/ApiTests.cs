using AddressBook.Api.Data;
using AddressBook.Api.Data.Entities;
using AddressBook.Api.Models;
using AddressBook.Apis;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Testcontainers.MsSql;

namespace AddressBook.Tests;

public class ApiTests(DatabaseFixture db) : IClassFixture<DatabaseFixture>
{

  [Fact]
  public async Task CanGetAllEntries()
  {
    var repository = db.Services.GetRequiredService<IBookRepository>();

    var result = await EntriesApi.GetEntries(repository);

    Assert.IsAssignableFrom<Ok<IEnumerable<BookEntryModel>>>(result);

    var data = ((Ok<IEnumerable<BookEntryModel>>)result).Value;
    Assert.NotNull(data);
    Assert.NotEmpty(data);
  }

  [Fact]
  public async Task CanGetEntry()
  {
    var repository = db.Services.GetRequiredService<IBookRepository>();

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
    var repository = db.Services.GetRequiredService<IBookRepository>();

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
    var repository = db.Services.GetRequiredService<IBookRepository>();

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