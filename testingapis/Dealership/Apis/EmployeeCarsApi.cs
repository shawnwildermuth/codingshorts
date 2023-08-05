using Dealership.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using WilderMinds.MinimalApiDiscovery;

namespace Dealership.Apis;

public class EmployeeCarsApi : IApi
{
  public void Register(WebApplication app)
  {
    var group = app.MapGroup("/api/employees/{empId:int}/cars")
      .AllowAnonymous();

    group.MapGet("", GetEmployeeCars);
    group.MapGet("{carId:int}", GetEmployeeCar);
  }

  static IResult GetEmployeeCars(Repo repo, int empId)
  {
    var result = repo.Cars
      .Where(c => c.SalesAssociate is not null && 
             c.SalesAssociate.Id == empId)
      .ToList();

    return Results.Ok(result);
  }

  static IResult GetEmployeeCar(Repo repo, int empId, int carId)
  {
    var result = repo.Cars
      .Where(c => c.SalesAssociate is not null &&
             c.SalesAssociate.Id == empId &&
             c.Id == carId)
      .FirstOrDefault();

    if (result is null) return Results.NotFound();

    return Results.Ok(result);
  }
}
