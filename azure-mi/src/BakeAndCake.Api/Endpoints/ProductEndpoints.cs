using Mapster;

namespace BakeAndCake.Api.Endpoints;

public static class ProductEndpoints
{
  public static void MapProductEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/api/products").WithTags("Products");

    group.MapGet("/", async (IProductRepository repo) =>
        Results.Ok((await repo.GetAllAsync()).Select(p => p.Adapt<ProductModel>())))
        .WithName("GetAllProducts");

    group.MapGet("/available", async (IProductRepository repo) =>
        Results.Ok((await repo.GetAvailableAsync()).Select(p => p.Adapt<ProductModel>())))
        .WithName("GetAvailableProducts");

    group.MapGet("/pie-of-the-week", async (IProductRepository repo) =>
    {
      var pie = await repo.GetPieOfTheWeekAsync();
      return pie is null ? Results.NotFound() : Results.Ok(pie.Adapt<ProductModel>());
    })
    .WithName("GetPieOfTheWeek")
    .WithSummary("Returns featured Pie of the Week");

    group.MapGet("/category/{category}", async (string category, IProductRepository repo) =>
    {
      if (!Enum.TryParse<ProductCategory>(category, ignoreCase: true, out var cat))
        return Results.BadRequest(new { message = $"Unknown category '{category}'." });
      return Results.Ok((await repo.GetByCategoryAsync(cat)).Select(p => p.Adapt<ProductModel>()));
    })
    .WithName("GetProductsByCategory");

    group.MapGet("/{id:int}", async (int id, IProductRepository repo) =>
    {
      var product = await repo.GetByIdAsync(id);
      return product is null ? Results.NotFound() : Results.Ok(product.Adapt<ProductModel>());
    })
    .WithName("GetProductById");

    group.MapPost("/", async (CreateProductModel dto, IProductRepository repo) =>
    {
      var product = dto.Adapt<Product>();
      var created = await repo.AddAsync(product);
      var full = await repo.GetWithIngredientsAsync(created.Id);
      return Results.Created($"/api/products/{created.Id}", full!.Adapt<ProductModel>());
    })
    .WithName("CreateProduct");

    group.MapPut("/{id:int}", async (int id, UpdateProductModel dto, IProductRepository repo) =>
    {
      var product = await repo.GetByIdAsync(id);
      if (product is null) return Results.NotFound();

      dto.Adapt(product);

      return Results.Ok((await repo.UpdateAsync(product)).Adapt<ProductModel>());
    })
    .WithName("UpdateProduct");

    // PATCH /api/products/{id}/availability
    group.MapPatch("/{id:int}/availability", async (int id, bool available, IProductRepository repo) =>
    {
      var ok = await repo.SetAvailabilityAsync(id, available);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("SetProductAvailability");

    // PATCH /api/products/{id}/pie-of-the-week
    group.MapPatch("/{id:int}/pie-of-the-week", async (int id, IProductRepository repo) =>
    {
      var ok = await repo.SetPieOfTheWeekAsync(id);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("SetPieOfTheWeek")
    .WithSummary("Promote this product to Pie of the Week (clears any previous selection)");

    group.MapDelete("/{id:int}", async (int id, IProductRepository repo) =>
    {
      var deleted = await repo.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteProduct");
  }

}
