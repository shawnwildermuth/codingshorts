namespace AddressBook.Api.Data.Entities;

public class BookEntry
{
  public int Id { get; set; }
  public string? CompanyName { get; set; }
  public string? FirstName { get; set; }
  public string? MiddleName { get; set; }
  public string? LastName { get; set; }
  public DateTime DateOfBirth { get; set; }
  public string? Gender { get; set; }
  public string? HomePhone { get; set; }
  public string? WorkPhone { get; set; }
  public string? CellPhone { get; set; }
  public string? Email { get; set; }
  public string? UserName { get; set; }

  public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
