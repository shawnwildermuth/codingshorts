using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using WilderMinds.SampleData.Bechdel;

namespace Bechdel.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FilmsController : ControllerBase
  {
    private readonly ILogger<FilmsController> _logger;
    private readonly BechdelContext _ctx;

    public FilmsController(ILogger<FilmsController> logger, BechdelContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    [HttpGet] 
    [OutputCache(VaryByQueryKeys = new[] { "passed" }, PolicyName = "ShortCache")]
    public async Task<ActionResult<IEnumerable<Film>>> Get(bool passed = true)
    {
      _logger.LogInformation($"Return Films from the Controller");

      _ctx.Database.EnsureCreated();    
      
      var results = await _ctx.Films
        .Where(f => f.Passed == passed)
        .OrderBy(f => f.Year)
        .ThenBy(f => f.Title)
        .ToListAsync();

      return Ok(results);
    }
  }
}
