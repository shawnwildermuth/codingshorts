using Microsoft.EntityFrameworkCore;
using ShoeMoney.Data;
using ShoeMoney.Data.Entities;
using MinimalApis.Discovery;

using static Microsoft.AspNetCore.Http.TypedResults;

namespace ShoeMoney.API.Apis;

public class ProductsApi : IApi
{
  public const int PAGE_SIZE = 24;

  public void Register(IEndpointRouteBuilder builder)
  {
    var group = builder.MapGroup("/api/products");

    group.MapGet("", GetProducts)
      .Produces<IEnumerable<Product>>()
      .ProducesProblem(500);

    group.MapGet("{id:int}", GetProduct)
      .Produces<Product>()
      .ProducesProblem(404)
      .ProducesProblem(500);
  }

  public static async Task<IResult> GetProducts(ShoeContext context, 
    int page = 1)
  {
    var results = await context.Products
      .Include(p => p.Category)
      .OrderBy(p => p.Title)
      .Skip(PAGE_SIZE * (page - 1))
      .Take(PAGE_SIZE)
      .ToListAsync();

    var count = await context.Products.CountAsync();
    var totalPages = float.Ceiling((float)count / (float)PAGE_SIZE);
      
    return Ok(new
    {
      currentPage = page,
      totalPages,
      results
    });
  }

  public static async Task<IResult> GetProduct(ShoeContext context, int id)
  {
    var result = await context.Products
      .Include(p => p.Category)
      .Where(p => p.Id == id)
      .FirstOrDefaultAsync();

    if (result is null) return NotFound();

    return Ok(result);
  }

}
