using System.Reflection;
using FluentValidation;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(cfg =>
{
  var configConnString = 
    builder.Configuration.GetConnectionString("AppConfiguration");
  cfg.Connect(configConnString);

    //.ConfigureRefresh(refresh =>
    //{
    //  refresh.Register("Auth:TenantId");
    //  var refresher = cfg.GetRefresher();
    //  builder.Services.AddSingleton(refresher);
    //});

    //.Select("dev:", "MainServer")
    //.TrimKeyPrefix("dev:");
});

// Add services to the container.
builder.Services.AddDbContext<BookContext>();
builder.Services.AddTransient<BookEntryFaker>();
builder.Services.AddTransient<AddressFaker>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddMapsterMaps();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapApis();

app.MapGet("/foo/bar", (IConfiguration config) =>
{
  var value = config["Auth:TenantId"];
  return Results.Ok(value);
});

app.MapFallbackToFile("/index.html");

app.Run();

