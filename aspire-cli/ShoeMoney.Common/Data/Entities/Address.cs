namespace ShoeMoney.Data.Entities;

public class Address
{
  public int Id { get; set; }
  public int OrderId { get; set; }
  public string? AttentionTo { get; set; }
  public string? Line1 { get; set; }
  public string? Line2 { get; set; }
  public string? CityTown { get; set; }
  public string? StateProvince { get; set; }
  public string? PostalCode { get; set; }
  public string? Country { get; set; }
  public string? ShippingPhoneNumber { get; set; }

}