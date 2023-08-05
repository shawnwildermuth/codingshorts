using JsonLocalization.Web.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<TextLocalizer>();

var app = builder.Build();

//app.UseRequestLocalization();

// Configure the HTTP request pipeline.
app.MapGet("/{locale}/{word}", 
  (TextLocalizer localizer, string locale, string word) =>
  {
    return localizer.Get(locale, word);
  });

app.MapRazorPages();

app.Run();

