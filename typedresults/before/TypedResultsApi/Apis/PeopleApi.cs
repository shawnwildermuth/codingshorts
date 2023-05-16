using System;
using TypedResultsApi.Data;

namespace TypedResultsApi.Apis;

public static class PeopleApi
{

  public static void Register(WebApplication app)
  {
    app.MapGet("/api/people", GetAllPeople);
    app.MapGet("/api/people/{id:int}", GetPerson);
    app.MapPost("/api/people", PostPerson);
  }

  public static IResult GetAllPeople(DataFactory factory)
  {
    var people = factory.GetAll();
    if (people is null || !people.Any()) return Results.NotFound();
    return Results.Ok(people);
  }

  public static IResult GetPerson(int id, DataFactory factory)
  {
    var person = factory.Get(id);
    if (person is null) return Results.NotFound(); 
    return Results.Ok(person);
  }

  public static IResult PostPerson(Person model, DataFactory factory)
  {
    var person = factory.Create(model);
    
    if (person is null) return Results.BadRequest();

    return Results.Created($"/api/person/{person.Id}", person);
  }
}
