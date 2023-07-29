var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/{locale}/{word}", (string locale, string word) =>
{
  return $"{word} - {locale}";
});

app.MapRazorPages();

app.Run();

