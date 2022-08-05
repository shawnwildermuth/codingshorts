using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WilderMinds.SampleData.Bechdel;

namespace Bechdel.Pages
{
  public class FilmModel : PageModel
  {
    private readonly ILogger<FilmModel> _logger;
    private readonly BechdelContext _ctx;

    public FilmModel(ILogger<FilmModel> logger, BechdelContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    public Film Film { get; set; } = new Film();

    public void OnGet(string id)
    {
      Film = _ctx.Films.Where(f => f.IMDBId == id).First();
      _logger.LogInformation($"Loaded: {Film.Title}");
    }
  }
}
