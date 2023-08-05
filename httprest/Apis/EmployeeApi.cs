using Dealership.Data;
using WilderMinds.MinimalApiDiscovery;

namespace Dealership.Apis;

public class EmployeeApi : IApi
{
  public void Register(WebApplication app)
  {
    var grp = app.MapGroup("api/employees")
      .CacheOutput();

    grp.MapGet("", GetEmployees).Produces(200, typeof(IEnumerable<Employee>));

    grp.MapGet("{id:int}", GetEmployee);


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
