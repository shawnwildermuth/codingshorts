using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalSecrets.Data;
using System.Linq;

namespace PersonalSecrets.Pages;

public class IndexModel : PageModel
{
  private readonly ILogger<IndexModel> _logger;
  private readonly SecretContext context;

  public IndexModel(ILogger<IndexModel> logger, SecretContext context)
  {
    _logger = logger;
    this.context = context;
  }

  public IEnumerable<Secret> Secrets { get; set; } = new List<Secret>();

  public async Task OnGetAsync()
  {
    Secrets = await context.Secrets.ToListAsync();
  }
}
