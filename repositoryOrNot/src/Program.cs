using RepoOrNot.Data;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<GenericRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
