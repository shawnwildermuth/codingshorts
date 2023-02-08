using VitePwa.Data;

namespace VitePwa;

public static class CustomerApis
{
  public static void MapCustomerApis(this WebApplication app)
  {
    app.MapGet("api/customers", (FakeCustomers customers) =>
    {
      return Results.Ok(customers.Generate(100));
    });
  }
}
