using Mapster;

namespace BakeAndCake.Api.Endpoints;

public static class CustomerEndpoints
{
  public static void MapCustomerEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/api/customers").WithTags("Customers");

    // GET /api/customers
    group.MapGet("/", async (ICustomerRepository repo) =>
        Results.Ok((await repo.GetAllAsync()).Select(c => c.Adapt<CustomerModel>())))
        .WithName("GetAllCustomers")
        .WithSummary("Get all customers, ordered by surname");

    // GET /api/customers/{id}
    group.MapGet("/{id:int}", async (int id, ICustomerRepository repo) =>
    {
      var c = await repo.GetByIdAsync(id);
      return c is null ? Results.NotFound() : Results.Ok(c.Adapt<CustomerModel>());
    })
    .WithName("GetCustomerById");

    // GET /api/customers/search?term=margaret
    group.MapGet("/search", async (string term, ICustomerRepository repo) =>
        Results.Ok((await repo.SearchAsync(term)).Select(c => c.Adapt<CustomerModel>())))
        .WithName("SearchCustomers")
        .WithSummary("Search by name, email or phone");

    // GET /api/customers/{id}/orders
    group.MapGet("/{id:int}/orders", async (int id, ICustomerRepository repo) =>
    {
      if (!await repo.ExistsAsync(id)) return Results.NotFound();
      return Results.Ok(await repo.GetOrdersByCustomerIdAsync(id));
    })
    .WithName("GetCustomerOrders");

    // POST /api/customers
    group.MapPost("/", async (CreateCustomerModel dto, ICustomerRepository repo) =>
    {
      if (await repo.GetByEmailAsync(dto.Email) is not null)
        return Results.Conflict(new ErrorModel { Message = "A customer with this email already exists." });

      var customer = dto.Adapt<Customer>();
      var created = await repo.AddAsync(customer);
      return Results.Created($"/api/customers/{created.Id}", created.Adapt<CustomerModel>());
    })
    .WithName("CreateCustomer");

    // PUT /api/customers/{id}
    group.MapPut("/{id:int}", async (int id, UpdateCustomerModel dto, ICustomerRepository repo) =>
    {
      var customer = await repo.GetByIdAsync(id);
      if (customer is null) return Results.NotFound();

      var clash = await repo.GetByEmailAsync(dto.Email);
      if (clash is not null && clash.Id != id)
        return Results.Conflict(new ErrorModel { Message = "Email already used by another customer." });

      dto.Adapt(customer);

      return Results.Ok((await repo.UpdateAsync(customer)).Adapt<CustomerModel>());
    })
    .WithName("UpdateCustomer");

    // PATCH /api/customers/{id}/loyalty
    group.MapPatch("/{id:int}/loyalty", async (int id, AdjustLoyaltyPointsModel dto, ICustomerRepository repo) =>
    {
      var ok = await repo.AdjustLoyaltyPointsAsync(id, dto.Points);
      return ok ? Results.NoContent() : Results.NotFound();
    })
    .WithName("AdjustLoyaltyPoints")
    .WithSummary("Add (positive) or redeem (negative) loyalty points");

    // DELETE /api/customers/{id}
    group.MapDelete("/{id:int}", async (int id, ICustomerRepository repo) =>
    {
      var deleted = await repo.DeleteAsync(id);
      return deleted ? Results.NoContent() : Results.NotFound();
    })
    .WithName("DeleteCustomer");
  }

}
