using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleChat.Data;

namespace SimpleChat.Controllers
{
  [Route("api/messages")]
  [ApiController]
  [ResponseCache(Duration = 60, 
                 Location = ResponseCacheLocation.Any,
                 VaryByQueryKeys = new string[] {"latest"})]
  public class MessagesController : ControllerBase
  {
    private readonly ILogger<MessagesController> _logger;
    private readonly ChatContext _ctx;

    public MessagesController(ILogger<MessagesController> logger,
      ChatContext ctx)
    {
      _logger = logger;
      _ctx = ctx;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> Get(bool latest = false)
    {
      _logger.LogInformation("Loading Messages");
      return Ok(await _ctx.Messages.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Message>> GetOne(int id, bool latest = false)
    {
      _logger.LogInformation("Loading Messages");
      return Ok(await _ctx.Messages.Where(m => m.Id == id).FirstAsync());
    }
  }
}
