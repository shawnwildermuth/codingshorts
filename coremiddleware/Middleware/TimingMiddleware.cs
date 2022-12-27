using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Middleware;

public class TimingMiddleware
{
  private readonly ILogger<TimingMiddleware> _logger;
  private readonly RequestDelegate _next;

  public TimingMiddleware(ILogger<TimingMiddleware> logger, 
    RequestDelegate next)
  {
    _logger = logger;
    _next = next;
  }

  public async Task Invoke(HttpContext ctx)
  {
    var start = DateTime.UtcNow;
    await _next.Invoke(ctx); // Pass the context
    _logger.LogInformation($"Timing: {ctx.Request.Path}: {(DateTime.UtcNow - start).TotalMilliseconds}ms");

  }

}

public static class TimingExtensions
{
  public static IApplicationBuilder UseTiming(this IApplicationBuilder app)
  {
    return app.UseMiddleware<TimingMiddleware>();
  }

  //public static void AddTiming(this IServiceCollection svcs)
  //{
  //  svcs.AddTransient<ITiming, SomeTiming>();
  //}
}
















