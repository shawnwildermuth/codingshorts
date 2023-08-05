namespace MappingTest.Models;

public class CustomerModel {

  public int Id {get;set;}
  public string CompanyName {get;set;} = "";
  public string? Phone {get;set;}
  public string? ContactName {get;set;}
  public string Address1 {get;set;} = "";
  public string? Address2 {get;set;}
  public string? Address3 {get;set;}
  public string City {get;set;} = "";
  public string StateProvince {get;set;} = "";
  public string PostalCode {get;set;} = "";
  public string? Country {get;set;}
  public ICollection<OrderModel>? Orders {get;set;}
}