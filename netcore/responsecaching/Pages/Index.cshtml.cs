using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleChat.Data;

namespace SimpleChat.Pages;

public class IndexModel : PageModel
{
  private readonly ILogger<IndexModel> _logger;
  private readonly ChatContext _ctx;

  public IndexModel(ILogger<IndexModel> logger, ChatContext ctx)
  {
    _logger = logger;
    _ctx = ctx;
  }

  public List<Message> Messages = new List<Message>();

  public void OnGet()
  {
    Messages.AddRange(_ctx.Messages.Include(m => m.Receiver).Include(m => m.Sender).ToList());
  }
}
