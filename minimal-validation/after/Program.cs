using MinimalApiValidation.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(PersonValidator));

var app = builder.Build();

var people = new[]
{
  new Person()
  {
    Id = 1,
    FullName = "Shawn Wildermuth",
    Phone = "",
    Email = "shawn@aol.com"
  }
};

app.MapGet("/api/people", () =>
{
  return Results.Ok(people);
});


app.MapGet("/api/people/1", () =>
{
  return Results.Ok(people[0]);
});


app.MapPost("/api/people", (Person person) =>
{
  // Fake some saving
  return Results.Created("/api/person/1", person);
})
  .AddEndpointFilter<ValidationFilter<Person>>();

app.Run();
