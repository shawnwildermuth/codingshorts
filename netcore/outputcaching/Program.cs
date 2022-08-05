using Microsoft.EntityFrameworkCore;
using WilderMinds.SampleData.Bechdel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddOutputCache(cfg => {
  cfg.AddBasePolicy(bldr => {
    bldr.With(r => r.HttpContext.Request.Path.StartsWithSegments("/api"));
    bldr.Expire(TimeSpan.FromSeconds(60));
  });

  cfg.AddPolicy("ShortCache", bldr => {
    bldr.Expire(TimeSpan.FromSeconds(5));
  });
});

builder.Services.AddDbContext<BechdelContext>(opt =>
{
  opt.UseInMemoryDatabase("Bechdel");
});
  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseOutputCache();

app.MapGet("/api/films/{year:int}", async (BechdelContext ctx, int year) =>
{
  app.Logger.LogInformation($"Reading films from {year} (Minimal API)");
  return await ctx.Films.Where(f => f.Year == year).ToListAsync();
}).CacheOutput();

app.MapRazorPages();
app.MapControllers();

app.Run();
