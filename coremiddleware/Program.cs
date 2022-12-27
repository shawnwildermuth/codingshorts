using Microsoft.EntityFrameworkCore;
using SimpleChat.Data;
using SimpleChat.Middleware;

var bldr = WebApplication.CreateBuilder(args);

bldr.Services.AddDbContext<ChatContext>(cfg => 
  cfg.UseSqlServer(bldr.Configuration.GetConnectionString("TestDb")));

// Add services to the container.
bldr.Services.AddRazorPages();

var app = bldr.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseTiming();

app.Use(async (ctx, next) =>
{
  var start = DateTime.UtcNow;
  await next.Invoke(ctx); // Pass the context

  // Execute code as the chain returns 
  // back up the list of middleware.
  var totalMs = (DateTime.UtcNow - start).TotalMilliseconds;
  app.Logger.LogInformation(
    @$"Request {ctx.Request.Path}: {totalMs}ms");
});

//app.Use((HttpContext ctx, Func<Task> next) =>
//{
//  app.Logger.LogInformation("Terminating the Request");
//  return Task.CompletedTask;
//});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
