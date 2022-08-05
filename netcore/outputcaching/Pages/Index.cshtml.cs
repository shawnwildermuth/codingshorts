using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using WilderMinds.SampleData.Bechdel;

namespace Bechdel.Pages
{
  [OutputCache]
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly BechdelContext _ctx;

    public IndexModel(ILogger<IndexModel> logger, BechdelContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    public List<Film> Films { get; set; } = new List<Film>();

    public void OnGet()
    {
      _logger.LogInformation("Loading entire film list");
      _ctx.Database.EnsureCreated();
      Films.AddRange(_ctx.Films.ToList());
    }
  }
}