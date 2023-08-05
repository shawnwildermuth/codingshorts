namespace Dealership.Data;

public class Vehicle
{
  public int Id { get; set; }
  public string? Make { get; set; }
  public string? Model { get; set; }
  public string? VehicleType { get; set; }
  public string? Vin { get; set; }
  public bool New { get; set; }
  public int Year { get; set; }
  public Employee? SalesAssociate { get; set; }
  public string? Picture { get; set; }
  public Lot? Lot { get; internal set; }
}