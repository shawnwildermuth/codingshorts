using Dealership.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using WilderMinds.MinimalApiDiscovery;

namespace Dealership.Apis;

public class EmployeeApi : IApi
{
  public void Register(WebApplication app)
  {
    var grp = app.MapGroup("api/employees")
      .CacheOutput();
      //.RequireAuthorization();

    grp.MapGet("", GetEmployees).Produces(200, typeof(IEnumerable<Employee>));
    grp.MapGet("{id:int}", GetEmployee)
      .WithName("GetEmployee");
    grp.MapPost("", CreateEmployee);
  }

  public static IResult CreateEmployee(Repo repo, Employee model)
  {
    model.Id = repo.Employees.Max(e => e.Id) + 1;
    repo.Employees.Add(model);

    return Results.CreatedAtRoute("GetEmployee", new { Id = model.Id }, model);
  }

  public static IResult GetEmployees(Repo repo)
  {
    if (repo.Employees.Count == 0) return Results.BadRequest();
    return Results.Ok(repo.Employees);
  }

  public static IResult GetEmployee(Repo repo, int id)
  {
    var emp = repo.Employees.Find(f => f.Id == id);
    if (emp is null) return Results.NotFound();
    return Results.Ok(emp);
  }
}
