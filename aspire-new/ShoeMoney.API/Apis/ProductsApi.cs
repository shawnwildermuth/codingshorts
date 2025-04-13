using Microsoft.EntityFrameworkCore;
using ShoeMoney.Data;
using ShoeMoney.Data.Entities;
using MinimalApis.Discovery;

using static Microsoft.AspNetCore.Http.TypedResults;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;

namespace ShoeMoney.API.Apis;

public class ProductsApi : IApi
{
  public const int PAGE_SIZE = 24;
  const string PRODUCT_CACHE = "PRODUCT_CACHE";

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
    IDistributedCache cache,
    int page = 1)
  {
    var cacheName = $"{PRODUCT_CACHE}_{page}";
    var cached = await cache.GetAsync(cacheName);
    if (cached is null)
    {
      var results = await context.Products
        .Include(p => p.Category)
        .OrderBy(p => p.Title)
        .Skip(PAGE_SIZE * (page - 1))
        .Take(PAGE_SIZE)
        .ToListAsync();

      var count = await context.Products.CountAsync();
      var totalPages = (int)float.Ceiling(count / PAGE_SIZE);

      var result = new ProductResult
      {
        CurrentPage = page,
        TotalPages = totalPages,
        Results = results
      };

      var newCache = JsonSerializer.Serialize(result);
      cached = Encoding.UTF8.GetBytes(newCache);
      await cache.SetAsync(cacheName, 
        cached, 
        new DistributedCacheEntryOptions()
        {
          SlidingExpiration = TimeSpan.FromMinutes(10)
        });
    }

    var json = Encoding.UTF8.GetString(cached);
    var final = JsonSerializer.Deserialize<ProductResult>(json);

    return Ok(final);
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

  private class ProductResult
  {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public required List<Product> Results { get;  set; }
  }
}
