using Dealership.Controllers;
using Dealership.Data;
using Dealership.Validators;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dealership.Tests.UnitTests;

public class CarsApiTests
{
  private CarsController _ctrl;

  public CarsApiTests()
  {
    var validator = new VehicleValidator();
    var repo = new Repo();
    _ctrl = new CarsController(repo, validator);
  }

  [Fact]
  public void CanGetCars()
  {
    var result = _ctrl.Get();

    Assert.IsAssignableFrom<Ok<List<Vehicle>>>(result);

    var value = ((Ok<List<Vehicle>>)result).Value;

    Assert.NotNull(value);
    Assert.NotEmpty(value);
  }
}
