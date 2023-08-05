namespace MappingTest.Entities;

public class Customer
{
  public int Id { get; set; }
  public string? CompanyName { get; set; }
  public string? Phone { get; set; }
  public string? ContactName { get; set; }
  public Address? Address { get;set;}
  public IEnumerable<Order>? Orders {get;set;}
}