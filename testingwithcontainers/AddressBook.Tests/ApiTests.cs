using AddressBook.Api.Data;
using AddressBook.Api.Data.Entities;
using AddressBook.Api.Models;
using AddressBook.Apis;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace AddressBook.Tests;

public class ApiTests
{
  private IBookRepository _repository;

  public ApiTests()
  {
    IEnumerable<BookEntry> entries = new BookEntryFaker(new AddressFaker()).Generate(10);

    var mock = new Mock<IBookRepository>();
    mock.Setup(p => p.GetAllEntries())
      .Returns(() => Task.FromResult(entries));
    mock.Setup(p => p.GetEntry(It.IsAny<int>()))
      .Returns((int id) => Task.FromResult(entries.Where(e => e.Id == id).FirstOrDefault()));

    _repository = mock.Object;
  }

  [Fact]
  public async Task CanGetAllEntries()
  {
    var result = await EntriesApi.GetEntries(_repository);

    Assert.IsAssignableFrom<Ok<IEnumerable<BookEntryModel>>>(result);
    
    var data = ((Ok<IEnumerable<BookEntryModel>>)result).Value;
    Assert.NotNull(data);
    Assert.NotEmpty(data);
  }

  [Fact]
  public async Task CanGetEntry()
  {
    var result = await EntriesApi.GetEntries(_repository);
    var data = ((Ok<IEnumerable<BookEntryModel>>)result).Value?.First();

    var entryResult = await EntriesApi.GetEntry(_repository, data!.Id);

    Assert.IsAssignableFrom<Ok<BookEntryModel>>(entryResult);

    var entry = ((Ok<BookEntryModel>)entryResult).Value;
    Assert.NotNull(entry);
    Assert.Equivalent(data, entry);
  }
}