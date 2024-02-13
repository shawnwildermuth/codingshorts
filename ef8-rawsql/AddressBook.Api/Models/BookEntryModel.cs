namespace AddressBook.Api.Models;

public class BookEntryModel
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

  public ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();

}
