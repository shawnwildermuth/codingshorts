using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace NewEfFeatures.Data;

public class FlightContext(IConfiguration config) : DbContext
{
  public DbSet<Flight> Flights => Set<Flight>();
  public DbSet<Passenger> Passengers => Set<Passenger>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    base.OnModelCreating(bldr);

    bldr.Entity<Flight>(act =>
    {
      act.Property(p => p.DepartureAirport).HasMaxLength(3);
      act.Property(p => p.ArrivalAirport).HasMaxLength(3);
      act.Property(p => p.Number).HasMaxLength(6);


      act.HasData(new Flight
      {
        Id = 1,
        Number = "SW0074",
        DepartureAirport = "ATL",
        ArrivalAirport = "AMS",
        Departure = new DateTime(2025, 2, 1, 15, 10, 0),
        FlightTime = TimeSpan.FromHours(7.5)
      },
      new Flight
      {
        Id = 2,
        Number = "SW0075",
        DepartureAirport = "AMS",
        ArrivalAirport = "ATL",
        Departure = new DateTime(2025, 2, 7, 15, 10, 0),
        FlightTime = TimeSpan.FromHours(8.5)
      });
    });

    bldr.Entity<Passenger>(act =>
    {
      act.Property(p => p.FirstName).HasMaxLength(25);
      act.Property(p => p.MiddleName).HasMaxLength(25);
      act.Property(p => p.LastName).HasMaxLength(50);
      act.Property(p => p.SeatNumber).HasMaxLength(5);

      act.HasData(new Passenger
      {
        Id = 1,
        FirstName = "Shawn",
        LastName = "Wildermuth",
        SeatNumber = "11B",
        Boarded = true,
        FlightId = 1
      },
      new Passenger
      {
        Id = 2,
        FirstName = "Jim",
        LastName = "Smith",
        SeatNumber = "31D",
        Boarded = true,
        FlightId = 1
      },
      new Passenger
      {
        Id = 3,
        FirstName = "Rachel",
        LastName = "Moore",
        SeatNumber = "19A",
        Boarded = true,
        FlightId = 1
      },
      new Passenger
      {
        Id = 4,
        FirstName = "Shawn",
        LastName = "Wildermuth",
        SeatNumber = "15F",
        Boarded = false,
        FlightId = 2
      },
      new Passenger
      {
        Id = 5,
        FirstName = "Kelley",
        LastName = "Montegue",
        SeatNumber = "2A",
        Boarded = false,
        FlightId = 2
      });
    });
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(config.GetConnectionString("FlightDb"));
  }
}