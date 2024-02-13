namespace AddressBook.Api.Data.Entities;

public class BookEntryLookup
{
  public int Id { get; set; }
  public required string FirstName { get; set; }
  public string? MiddleName { get; set; }
  public required string LastName { get; set; }
  public string? CompanyName { get; set; }
}
