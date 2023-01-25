using FakingIt.Entities;
using FakingIt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FakingIt.Pages;

public class IndexModel : PageModel
{
  private readonly ILogger<IndexModel> _logger;
  private readonly FakingContext _ctx;

  public IndexModel(ILogger<IndexModel> logger, FakingContext ctx)
  {
    _logger = logger;
    _ctx = ctx;
  }

  public IEnumerable<Customer> Customers = new List<Customer>();

  public void OnGet()
  {
    Customers = _ctx.Customers.Include(c => c.Address).ToArray();
  }
}
