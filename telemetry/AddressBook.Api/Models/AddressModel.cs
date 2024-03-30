namespace AddressBook.Api.Models;

public class AddressModel
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Line1 { get; set; }
  public string? Line2 { get; set; }
  public string? Line3 { get; set; }
  public string? CityTown { get; set; }
  public string? StateProvince { get; set; }
  public string? PostalCode { get; set; }
  public string? Country { get; set; }
}
