using Dealership.Data;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Dealership.Controllers;

[Route("api/cars")]
[ApiController]
public class CarsController
{
  private readonly Repo _repo;
  private readonly IValidator<Vehicle> _validator;

  public CarsController(Repo repo, IValidator<Vehicle> validator)
  {
    _repo = repo;
    _validator = validator;
  }

  public IResult Get()
  {
    if (_repo.Cars.Count == 0) return TypedResults.BadRequest();
    return TypedResults.Ok(_repo.Cars);
  }

  [HttpGet("{id:int}", Name = "GetCar")]
  public IResult Get(int id)
  {
    var result = _repo.Cars.Find(v => v.Id == id);
    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  [HttpGet("{vin:length(17)}")]
  public IResult Get(string vin)
  {
    var result = _repo.Cars.Find(v => v.Vin == vin);
    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  [HttpPost()]
  public IResult Post(Vehicle model)
  {
    var valid = _validator.Validate(model);
    if (!valid.IsValid) return Results.ValidationProblem(valid.ToDictionary());

    model.Id = _repo.Cars.Max(a => a.Id) + 1;
    _repo.Cars.Add(model);

    return Results.CreatedAtRoute("GetCar", new { id = model.Id }, model);
  }

  [HttpPut("{id:int}")]
  public IResult Put(int id, Vehicle model)
  {
    var valid = _validator.Validate(model);
    if (!valid.IsValid) return Results.ValidationProblem(valid.ToDictionary());

    var old = _repo.Cars.Find(f => f.Id == id);
    if (old is null) return Results.NotFound();

    model.Adapt(old);
    return Results.Ok(old);
  }

  [HttpDelete("{id:int}")]
  public IResult Delete(int id)
  {
    var old = _repo.Cars.Find(f => f.Id == id);
    if (old is null) return Results.NotFound();

    _repo.Cars.Remove(old);

    return Results.Ok();
  }
}
