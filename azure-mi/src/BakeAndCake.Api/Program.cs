using System.Reflection;
using BakeAndCake.Api.Data;
using BakeAndCake.Api.Endpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BakeAndCakeDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BakeAndCakeDb"),
        sqlOptions =>
        {
          sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
          sqlOptions.CommandTimeout(30);
        }));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomPieRepository, CustomPieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new()
  {
    Title = "Bake and Cake API",
    Version = "v1",
    Description = """
            Point-of-Sale REST API for Pie Shop.
            Covers customers, ingredients, products (pies, tarts, quiches),
            custom pie orders, POS orders with VAT & loyalty points, and receipts.
            """
  });
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddProblemDetails();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bake and Cake API v1");
    c.DocumentTitle = "Bake and Cake API";
  });

  // Auto-apply EF Core migrations on startup (dev only)
  using var scope = app.Services.CreateScope();
  var db = scope.ServiceProvider.GetRequiredService<BakeAndCakeDbContext>();
  await db.Database.MigrateAsync();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapCustomerEndpoints();
app.MapIngredientEndpoints();
app.MapProductEndpoints();
app.MapCustomPieEndpoints();
app.MapOrderEndpoints();
app.MapReceiptEndpoints();

// Health check
app.MapGet("/health", () => Results.Ok(new
{
  shop = "Bake and Cake",
  status = "Baking beautifully 🥧",
  timestamp = DateTime.UtcNow
}))
.WithTags("Health");

app.Run();

// Expose Program for integration tests
public partial class Program { }
