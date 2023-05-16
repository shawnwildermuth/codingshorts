using Microsoft.AspNetCore.Mvc;
using TypedResultsApi.Data;

namespace TypedResultsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
  private readonly DataFactory _factory;

  public JobsController(DataFactory factory)
    {
    _factory = factory;
  }

  [HttpGet]
  public IResult Get()
  {
    var result = _factory.GetAllJobs();
    if (result is null || !result.Any()) return Results.NotFound();
    return Results.Ok(result);
  }
}
