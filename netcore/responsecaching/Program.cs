using Microsoft.EntityFrameworkCore;
using SimpleChat.Data;

var bldr = WebApplication.CreateBuilder(args);

bldr.Services.AddDbContext<ChatContext>(cfg => 
  cfg.UseSqlServer(bldr.Configuration.GetConnectionString("TestDb")));

bldr.Services.AddControllers();
bldr.Services.AddRazorPages();
bldr.Services.AddResponseCaching();

var app = bldr.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();
app.MapRazorPages();

app.Run();
