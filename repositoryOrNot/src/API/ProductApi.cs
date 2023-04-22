using Microsoft.AspNetCore.Mvc;
using RepoOrNot.Data;
using RepoOrNot.Data.Entities;
using WilderMinds.MinimalApiDiscovery;

namespace RepoOrNot.API;

[Route("api/products")]
[ApiController]
public class ProductsApi : ControllerBase
{
  private readonly GenericRepository _repo;

  public ProductsApi(GenericRepository repo)
  {
    _repo = repo;
  }

  public async Task<IResult> Get()
  {
    var products = await _repo.Get<Product>()
                              .OrderBy(p => p.UnitPrice)
                              .ToListAsync();
    if (!products.Any())
    {
      return Results.NotFound();
    }

    return Results.Ok(products);
  }

}
