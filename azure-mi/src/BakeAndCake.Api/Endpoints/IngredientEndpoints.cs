using Mapster;

namespace BakeAndCake.Api.Endpoints;

public static class IngredientEndpoints
{
  public static void MapIngredientEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/api/ingredients").WithTags("Ingredients");

    group.MapGet("/", async (IIngredientRepository repo) =>
        Results.Ok((await repo.GetAllAsync()).Select(i => i.Adapt<IngredientModel>())))
        .WithName("GetAllIngredients");

    group.MapGet("/low-stock", async (IIngredientRepository repo) =>
        Results.Ok((await repo.GetLowStockAsync()).Select(i => i.Adapt<IngredientModel>())))
        .WithName("GetLowStockIngredients")
        .WithSummary("Ingredients at or below their reorder threshold");

    group.MapGet("/allergens", async (IIngredientRepository repo) =>
        Results.Ok((await repo.GetAllergensAsync()).Select(i => i.Adapt<IngredientModel>())))
        .WithName("GetAllergens");

    group.MapGet("/{id:int}", async (int id, IIngredientRepository repo) =>
    {
      var i = await repo.GetByIdAsync(id);
      return i is null ? Results.NotFound() : Results.Ok(i.Adapt<IngredientModel>());
    })
    .WithName("GetIngredientById");

    group.MapPost("/", async (CreateIngredientModel dto, IIngredientRepository repo) =>
    {
      var ingredient = dto.Adapt<Ingredient>();
      var created = await repo.AddAsync(ingredient);
      return Results.Created($"/api/ingredients/{created.Id}", created.Adapt<IngredientModel>());
    })
    .WithName("CreateIngredient");

    group.MapPut("/{id:int}", async (int id, UpdateIngredientModel dto, IIngredientRepository repo) =>
    {
      var ingredient = await repo.GetByIdAsync(id);
      if (ingredient is null) return Results.NotFound();

      dto.Adapt(ingredient);

      return Results.Ok((await repo.UpdateAsync(ingredient)).Adapt<IngredientModel>());
    })
    .WithName("UpdateIngredient");

    // PATCH /api/ingredients/{id}/stock
    group.MapPatch("/{id:int}/stock", async (int id, AdjustStockModel dto, IIngredientRepository repo) =>
    {
      var ok = await repo.AdjustStockAsync(id, dto.Quantity);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("AdjustIngredientStock")
    .WithSummary("Adjust stock: positive = restock, negative = consume");

    group.MapDelete("/{id:int}", async (int id, IIngredientRepository repo) =>
    {
      var deleted = await repo.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteIngredient");
  }

}
