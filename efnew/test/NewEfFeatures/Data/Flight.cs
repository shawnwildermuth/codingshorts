namespace NewEfFeatures.Data;

public class Flight
{
  public int Id { get; set; }
  public required string Number { get; set; }
  public required string DepartureAirport { get; set; }
  public required string DestinationAirport { get; set; }
  public string? Gate { get; set; }
  public required DateTime Departure { get; set; }
  public required TimeSpan FlightTime { get; set; }

  public virtual List<Passenger> Passengers { get; set; } = new();
}