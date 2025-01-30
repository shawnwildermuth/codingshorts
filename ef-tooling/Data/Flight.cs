namespace NewEfFeatures.Data;

public class Flight
{
  public int Id { get; set; }
  public string? Nickname { get; set; }
  public required string Number { get; set; }
  public required string DepartureAirport { get; set; }
  public required string ArrivalAirport { get; set; }

  public required DateTime Departure { get; set; }
  public required TimeSpan FlightTime { get; set; }

  public virtual List<Passenger> Passengers { get; set; } = new();
}