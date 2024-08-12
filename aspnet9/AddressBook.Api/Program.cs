using System.Reflection;
using FluentValidation;
using MinimalApis.Discovery;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookContext>();
builder.Services.AddTransient<BookEntryFaker>();
builder.Services.AddTransient<AddressFaker>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddMapsterMaps();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapApis();

app.MapFallbackToFile("/index.html");

app.Run();

