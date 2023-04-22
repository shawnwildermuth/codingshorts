using Microsoft.AspNetCore.Mvc;
using RepoOrNot.Data;
using RepoOrNot.Data.Entities;
using WilderMinds.MinimalApiDiscovery;

namespace RepoOrNot.API;

[Route("api/orders")]
[ApiController]
public class OrdersApi : ControllerBase
{
  private readonly IStoreRepository _repo;

  public OrdersApi(IStoreRepository repo)
  {
    _repo = repo;
  }

  public async Task<IResult> Get(string? productName)
  {
    var orders = productName is null ?
                 await _repo.GetOrders() :
                 await _repo.GetOrdersByProductName(productName!);


    if (!orders.Any()) return Results.NotFound();
    return Results.Ok(orders);

  }
}
