var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/api/startprocess", () => Results.Accepted("Process Started"));

app.Run();
