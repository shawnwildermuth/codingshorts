using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
  [ApiController]
  [Route("/api/person")]
  public class PersonController: ControllerBase
  {
    [HttpGet]
    public IResult Get()
    {
      return Results.Ok("Testing");
    }
  }
}