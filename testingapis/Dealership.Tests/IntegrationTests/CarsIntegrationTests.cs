using System.Text.Json;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Dealership.Tests.IntegrationTests;

public class CarsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
  private WebApplicationFactory<Program> _factory;

  public CarsIntegrationTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task CanReadCars()
  {
    var client = _factory.CreateClient();

    var result = await client.GetAsync("/api/cars");

    Assert.NotNull(result);

    Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);

    var content = await result.Content.ReadAsStringAsync();

    Assert.NotEmpty(content);

    var cars = JsonSerializer.Deserialize<IEnumerable<Vehicle>>(content);

    Assert.NotNull(cars);

    Assert.NotEmpty(cars);

  }
}
