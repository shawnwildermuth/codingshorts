using Mapster;

namespace BakeAndCake.Api.Endpoints;

public static class CustomPieEndpoints
{
  public static void MapCustomPieEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/api/custom-pies").WithTags("Custom Pies");

    group.MapGet("/", async (ICustomPieRepository repo) =>
        Results.Ok((await repo.GetAllAsync()).Select(cp => cp.Adapt<CustomPieModel>())))
        .WithName("GetAllCustomPies");

    group.MapGet("/pending-approval", async (ICustomPieRepository repo) =>
        Results.Ok((await repo.GetPendingApprovalAsync()).Select(cp => cp.Adapt<CustomPieModel>())))
        .WithName("GetCustomPiesPendingApproval")
        .WithSummary("Custom pie orders awaiting approval, soonest deadline first");

    group.MapGet("/customer/{customerId:int}", async (int customerId, ICustomPieRepository repo) =>
        Results.Ok((await repo.GetByCustomerAsync(customerId)).Select(cp => cp.Adapt<CustomPieModel>())))
        .WithName("GetCustomPiesByCustomer");

    group.MapGet("/{id:int}", async (int id, ICustomPieRepository repo) =>
    {
      var pie = await repo.GetByIdAsync(id);
      return pie is null ? Results.NotFound() : Results.Ok(pie.Adapt<CustomPieModel>());
    })
    .WithName("GetCustomPieById");

    group.MapPost("/", async (CreateCustomPieModel dto, ICustomPieRepository repo) =>
    {
      var pie = dto.Adapt<CustomPie>();
      var created = await repo.AddAsync(pie);
      var full = await repo.GetWithIngredientsAsync(created.Id);
      return Results.Created($"/api/custom-pies/{created.Id}", full!.Adapt<CustomPieModel>());
    })
    .WithName("CreateCustomPie")
    .WithSummary("Submit a custom pie order for review");

    group.MapPut("/{id:int}", async (int id, UpdateCustomPieModel dto, ICustomPieRepository repo) =>
    {
      var pie = await repo.GetByIdAsync(id);
      if (pie is null) return Results.NotFound();
      if (pie.IsApproved)
        return Results.Conflict(new ErrorModel { Message = "Approved custom pies cannot be edited." });

      dto.Adapt(pie);

      return Results.Ok((await repo.UpdateAsync(pie)).Adapt<CustomPieModel>());
    })
    .WithName("UpdateCustomPie");

    // POST /api/custom-pies/{id}/approve
    group.MapPost("/{id:int}/approve", async (int id, ApproveCustomPieModel dto, ICustomPieRepository repo) =>
    {
      if (dto.EstimatedPrice <= 0)
        return Results.BadRequest(new { message = "Estimated price must be greater than zero." });
      var ok = await repo.ApproveAsync(id, dto.EstimatedPrice);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("ApproveCustomPie")
    .WithSummary("Approve a custom pie design and set the price quote");

    group.MapDelete("/{id:int}", async (int id, ICustomPieRepository repo) =>
    {
      var deleted = await repo.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteCustomPie");
  }

}
