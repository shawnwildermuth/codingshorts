using MinimalApis.Discovery;

var builder = WebApplication.CreateBuilder(args);

bool isTesting = builder.Configuration.GetValue<bool>("IsTesting", true);

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<BechdelDataService>();

builder.Services.AddCors();

builder.Services.AddOpenApi();

//builder.Services.AddSwaggerGen(setup =>
//{
//  if (!isTesting)
//  {
//    var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"BechdelDataServer.xml"));
//    setup.IncludeXmlComments(path);
//  }
  
//  setup.SwaggerDoc("v1", new OpenApiInfo()
//  {
//    Description = "Bechdel Test API using data from FiveThirtyEight.com",
//    Title = "Bechdel Test API",
//    Version = "v1"
//  });

//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors(cfg =>
{
  cfg.WithMethods("GET");
  cfg.AllowAnyHeader();
  cfg.AllowAnyOrigin();
});

//app.UseSwagger();
//app.UseSwaggerUI();

app.MapOpenApi("/docs/api/openapi.json");

app.MapApis();

app.Run();

// To enable access to the Top Level Class
public partial class Program { }
