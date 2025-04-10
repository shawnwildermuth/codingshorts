using Microsoft.EntityFrameworkCore;

using ShoeMoney.Data;
using ShoeMoney.Data.Entities;

using MinimalApis.Discovery;

namespace ShoeMoney.API.Apis;

using static Microsoft.AspNetCore.Http.TypedResults;

public class CategoriesApi : IApi
{
  public void Register(IEndpointRouteBuilder builder)
  {
    var group = builder.MapGroup("/api/categories");

    group.MapGet("", GetCategories)
      .Produces<IEnumerable<Category>>()
      .ProducesProblem(500);

    group.MapGet("{id:int}/products", GetProductsByCategories)
      .Produces<IEnumerable<Product>>()
      .ProducesProblem(404)
      .ProducesProblem(500);

  }

  public static async Task<IResult> GetCategories(ShoeContext context)
  {
    var results = await context.Categories
      .OrderBy(c => c.Name)
      .ToListAsync();

    return Ok(results);
  }

  public static async Task<IResult> GetProductsByCategories(ShoeContext context, 
    int id, 
    int page = 1)
  {
    var results = await context.Products
      .Where(p => p.CategoryId == id)
      .Include(p => p.Category)
      .OrderBy(p => p.Title)
      .Skip(ProductsApi.PAGE_SIZE * (page - 1))
      .Take(ProductsApi.PAGE_SIZE)
      .ToListAsync();

    if (!results.Any()) return NotFound();

    var count = await context.Products
      .Where(p => p.CategoryId == id)
      .CountAsync();
    var totalPages = double.Ceiling(count / ProductsApi.PAGE_SIZE);

    return Ok(new
    {
      currentPage = page,
      totalPages,
      results
    });
  }
}
