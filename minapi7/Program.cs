var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Groups
// Filters

var person = new Person("Shawn");

var personGroup = app.MapGroup("/api/people")
  .CacheOutput()
  // .RequireAuthorization()
  // .RequireCors("FooBar")
  .WithGroupName("people")
  .AddEndpointFilter<CustomHeaderFilter>();

personGroup.MapGet("", () => new Person[] { person });
personGroup.MapGet("{id:int}", (int id) => person);

app.Run();

public class CustomHeaderFilter : IEndpointFilter
{
  private readonly ILogger<CustomHeaderFilter> logger;

  public CustomHeaderFilter(ILogger<CustomHeaderFilter> logger)
  {
    this.logger = logger;
  }
  public async ValueTask<object?> InvokeAsync(
    EndpointFilterInvocationContext ctx, 
    EndpointFilterDelegate next)
  {
    // if (ctx.HttpContext.Request.Headers.ContainsKey("X-My-Secret-Header"))
    // {
      logger.LogInformation("Before next");
      var result = await next(ctx);
      logger.LogInformation("After next");

      return result;
    // }
    // else 
    // {
    //   return Results.Problem("Header is missing");
    // }
  }
}

public class Person
{
  public Person(string name)
  {
    Name = name;
  }

  public string Name { get; init; }
}