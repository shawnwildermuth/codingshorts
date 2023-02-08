namespace VitePwa.Data;

public class Customer
{
  public int Id { get; set; }
  public string? CompanyName { get; set; } = null;
  public string? Contact{ get; set; } = null;
  public string? Phone { get; set; } = null;
  public string? WebAddress { get; set; } = null;
  public string? Address1{ get; set; } = null;
  public string? Address2 { get; set; } = null;
  public string? Address3 { get; set; } = null;
  public string? City { get; set; } = null;
  public string? State { get; set; } = null;
  public string? PostalCode { get; set; } = null;
  public decimal CreditLimit { get; set; }
}