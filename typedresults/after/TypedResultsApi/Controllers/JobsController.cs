using Microsoft.AspNetCore.Http.HttpResults;
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
  public Results<Ok<IEnumerable<Job>>, NotFound, BadRequest> Get()
  {
    var result = _factory.GetAllJobs();
    if (result is null || !result.Any()) return TypedResults.NotFound();
    return TypedResults.Ok(result);
  }
}
