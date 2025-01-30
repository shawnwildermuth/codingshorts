namespace NewEfFeatures.Data;

public class Passenger
{
  public int Id { get; set; }
  public required string FirstName { get; set; }
  public string? MiddleName { get; set; }
  public required string LastName { get; set; }
  public required string SeatNumber { get; set; }
  public bool Boarded { get; set; }
  public int FlightId { get; set; }
}