using Microsoft.EntityFrameworkCore;
using MinimalApis.Discovery;
using NewEfFeatures.Data;
using static Microsoft.AspNetCore.Http.Results;
namespace NewEfFeatures.Apis;

public class PassengerApi : IApi
{
  public void Register(IEndpointRouteBuilder builder)
  {
    var grp = builder.MapGroup("/api/flights/{flightNumber}/passengers")
      .WithDisplayName("FlightPassengers")
      .WithOpenApi();

    grp.MapGet("", GetAllPassengers);

    grp.MapGet("{seat}", GetPassenger);
  }

  public static async Task<IResult> GetAllPassengers(FlightContext context)
  {
    return Ok(await context.Flights
      .Include(c => c.Passengers)
      .OrderBy(f => f.Departure)
      .ToListAsync());
  }
  public static async Task<IResult> GetPassenger(FlightContext context, string num)
  {
    var result = await context.Flights
      .Include(c => c.Passengers)
      .Where(f => f.Number == num)
      .FirstOrDefaultAsync();
    
    if (result is null) return NotFound();
    
    return Ok(result);
  }

}
