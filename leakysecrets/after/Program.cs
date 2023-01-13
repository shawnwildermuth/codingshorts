using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Config
builder.Configuration.Sources.Clear();
builder.Configuration
  .AddJsonFile("appsettings.json")
  .AddJsonFile("appsettings.Development.json", false)
  .AddUserSecrets(Assembly.GetEntryAssembly()!)
  .AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
