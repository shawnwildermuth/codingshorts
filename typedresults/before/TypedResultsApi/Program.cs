using TypedResultsApi.Apis;
using TypedResultsApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<DataFactory>();

var app = builder.Build();

PeopleApi.Register(app);

app.MapControllers();

app.Run();

