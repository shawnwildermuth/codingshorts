namespace AddressBook.Api.Models;

public class BookModel
{
  public required IEnumerable<BookEntryModel> BookEntries { get; set; }
}
