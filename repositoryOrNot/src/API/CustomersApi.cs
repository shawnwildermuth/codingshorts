using Microsoft.AspNetCore.Mvc;
using RepoOrNot.Data;
using RepoOrNot.Data.Entities;
using WilderMinds.MinimalApiDiscovery;

namespace RepoOrNot.API;

[Route("api/customers")]
[ApiController]
public class CustomersApi : ControllerBase
{
  private readonly StoreContext _ctx;

  public CustomersApi(StoreContext ctx)
  {
    _ctx = ctx;
  }

  [HttpGet]
  public async Task<IResult> Get(bool? withOrders)
  {
    IEnumerable<Customer>? result = null;
    if (withOrders is true)
    {
      result = await _ctx.Customers
                         .Include(c => c.Orders)
                         .ThenInclude(o => o.OrderItems)
                         .OrderBy(c => c.CompanyName)
                         .ToArrayAsync();
    }
    else
    {
      result = await _ctx.Customers
                         .OrderBy(c => c.CompanyName)
                         .ToArrayAsync();
    }

    return Results.Ok(result);
  }

  [HttpGet("{id:int}")]
  public async Task<IResult> GetOne(int id)
  {
    var customer = await _ctx.Customers.FindAsync(id);
    if (customer is null) return Results.NotFound();

    return Results.Ok(customer);
  }
}
