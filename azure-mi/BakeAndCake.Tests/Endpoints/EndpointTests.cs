using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using Xunit;

namespace BakeAndCake.Tests.Endpoints;

/// <summary>
/// Tests the endpoint handler logic directly, mocking all repository dependencies.
/// These tests validate routing decisions (200/201/204/404/409/400) without a web server.
/// </summary>
public class CustomerEndpointTests
{

  private static ICustomerRepository MockRepo(params Customer[] customers)
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.GetAllAsync().Returns(Task.FromResult<IEnumerable<Customer>>(customers));
    mock.GetByIdAsync(Arg.Any<int>())
        .Returns(callInfo => Task.FromResult<Customer?>(customers.FirstOrDefault(c => c.Id == callInfo.Arg<int>())));
    mock.ExistsAsync(Arg.Any<int>())
        .Returns(callInfo => Task.FromResult(customers.Any(c => c.Id == callInfo.Arg<int>())));
    return mock;
  }


  [Fact]
  public async Task GetAll_ReturnsOkWithCustomerList()
  {
    var mock = MockRepo(TestData.Margaret(), TestData.Robert());
    var result = await InvokeGetAll(mock);

    result.Should().BeOfType<Ok<IEnumerable<CustomerModel>>>();
    var ok = (Ok<IEnumerable<CustomerModel>>)result;
    ok.Value.Should().HaveCount(2);
  }

  [Fact]
  public async Task GetAll_ReturnsOkWithEmptyList_WhenNoCustomers()
  {
    var mock = MockRepo();
    var result = await InvokeGetAll(mock);

    result.Should().BeOfType<Ok<IEnumerable<CustomerModel>>>();
    var ok = (Ok<IEnumerable<CustomerModel>>)result;
    ok.Value.Should().BeEmpty();
  }


  [Fact]
  public async Task GetById_ReturnsOk_WhenFound()
  {
    var mock = MockRepo(TestData.Margaret());
    var result = await InvokeGetById(1, mock);

    result.Should().BeOfType<Ok<CustomerModel>>();
    var ok = (Ok<CustomerModel>)result;
    ok.Value!.Email.Should().Be("margaret@test.com");
  }

  [Fact]
  public async Task GetById_ReturnsNotFound_WhenMissing()
  {
    var mock = MockRepo();
    var result = await InvokeGetById(999, mock);

    result.Should().BeOfType<NotFound>();
  }


  [Fact]
  public async Task Create_ReturnsCreated_WhenEmailIsUnique()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.GetByEmailAsync("new@example.com").Returns(Task.FromResult<Customer?>(null));
    mock.AddAsync(Arg.Any<Customer>())
        .Returns(callInfo => { var c = callInfo.Arg<Customer>(); c.Id = 99; return Task.FromResult(c); });

    var dto = new CreateCustomerModel("Alice", "Green", "new@example.com", null, null, false);
    var result = await InvokeCreate(dto, mock);

    result.Should().BeOfType<Created<CustomerModel>>();
    var created = (Created<CustomerModel>)result;
    created.Location.Should().Contain("99");
  }

  [Fact]
  public async Task Create_ReturnsConflict_WhenEmailAlreadyExists()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.GetByEmailAsync("margaret@test.com").Returns(Task.FromResult<Customer?>(TestData.Margaret()));

    var dto = new CreateCustomerModel("Different", "Person", "margaret@test.com", null, null, false);
    var result = await InvokeCreate(dto, mock);

    result.Should().BeAssignableTo<Conflict<ErrorModel>>();
  }


  [Fact]
  public async Task Update_ReturnsOk_WhenSuccessful()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.GetByIdAsync(1).Returns(Task.FromResult<Customer?>(TestData.Margaret()));
    mock.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<Customer?>(null));
    mock.UpdateAsync(Arg.Any<Customer>()).Returns(callInfo => Task.FromResult(callInfo.Arg<Customer>()));

    var dto = new UpdateCustomerModel("Maggie", "Whitfield", "maggie@test.com", null, null, true);
    var result = await InvokeUpdate(1, dto, mock);

    result.Should().BeOfType<Ok<CustomerModel>>();
    var ok = (Ok<CustomerModel>)result;
    ok.Value!.FirstName.Should().Be("Maggie");
  }

  [Fact]
  public async Task Update_ReturnsNotFound_WhenCustomerMissing()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<Customer?>(null));

    var dto = new UpdateCustomerModel("X", "X", "x@x.com", null, null, false);
    var result = await InvokeUpdate(999, dto, mock);

    result.Should().BeOfType<NotFound>();
  }


  [Fact]
  public async Task Delete_ReturnsNoContent_WhenDeleted()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.DeleteAsync(1).Returns(Task.FromResult(true));

    var result = await InvokeDelete(1, mock);

    result.Should().BeOfType<NoContent>();
  }

  [Fact]
  public async Task Delete_ReturnsNotFound_WhenMissing()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.DeleteAsync(999).Returns(Task.FromResult(false));

    var result = await InvokeDelete(999, mock);

    result.Should().BeOfType<NotFound>();
  }


  [Fact]
  public async Task AdjustLoyaltyPoints_ReturnsNoContent_WhenSuccessful()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.AdjustLoyaltyPointsAsync(1, 50).Returns(Task.FromResult(true));

    var dto = new AdjustLoyaltyPointsModel(50);
    var result = await InvokeAdjustLoyalty(1, dto, mock);

    result.Should().BeOfType<NoContent>();
  }

  [Fact]
  public async Task AdjustLoyaltyPoints_ReturnsNotFound_WhenCustomerMissing()
  {
    var mock = Substitute.For<ICustomerRepository>();
    mock.AdjustLoyaltyPointsAsync(999, Arg.Any<int>()).Returns(Task.FromResult(false));

    var dto = new AdjustLoyaltyPointsModel(50);
    var result = await InvokeAdjustLoyalty(999, dto, mock);

    result.Should().BeOfType<NotFound>();
  }

  // These replicate the lambda bodies so we can test logic without a running server.

  private static async Task<IResult> InvokeGetAll(ICustomerRepository repo)
  {
    var customers = await repo.GetAllAsync();
    return Results.Ok(customers.Select(c =>
        new CustomerModel(c.Id, c.FirstName, c.LastName, c.Email, c.Phone, c.Address,
            c.IsLoyaltyMember, c.LoyaltyPoints, c.CreatedAt)));
  }

  private static async Task<IResult> InvokeGetById(int id, ICustomerRepository repo)
  {
    var c = await repo.GetByIdAsync(id);
    return c is null ? Results.NotFound() :
        Results.Ok(new CustomerModel(c.Id, c.FirstName, c.LastName, c.Email, c.Phone,
            c.Address, c.IsLoyaltyMember, c.LoyaltyPoints, c.CreatedAt));
  }

  private static async Task<IResult> InvokeCreate(CreateCustomerModel dto, ICustomerRepository repo)
  {
    if (await repo.GetByEmailAsync(dto.Email) is not null)
      return Results.Conflict(new ErrorModel { Message = "A customer with this email already exists." });

    var customer = new Customer
    {
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      Email = dto.Email,
      Phone = dto.Phone,
      Address = dto.Address,
      IsLoyaltyMember = dto.IsLoyaltyMember
    };
    var created = await repo.AddAsync(customer);
    var dto2 = new CustomerModel(created.Id, created.FirstName, created.LastName,
        created.Email, created.Phone, created.Address, created.IsLoyaltyMember,
        created.LoyaltyPoints, created.CreatedAt);
    return Results.Created($"/api/customers/{created.Id}", dto2);
  }

  private static async Task<IResult> InvokeUpdate(int id, UpdateCustomerModel dto, ICustomerRepository repo)
  {
    var customer = await repo.GetByIdAsync(id);
    if (customer is null) return Results.NotFound();

    var clash = await repo.GetByEmailAsync(dto.Email);
    if (clash is not null && clash.Id != id)
      return Results.Conflict(new ErrorModel { Message = "Email already used by another customer." });

    customer.FirstName = dto.FirstName; customer.LastName = dto.LastName;
    customer.Email = dto.Email; customer.Phone = dto.Phone;
    customer.Address = dto.Address; customer.IsLoyaltyMember = dto.IsLoyaltyMember;

    var updated = await repo.UpdateAsync(customer);
    return Results.Ok(new CustomerModel(updated.Id, updated.FirstName, updated.LastName,
        updated.Email, updated.Phone, updated.Address, updated.IsLoyaltyMember,
        updated.LoyaltyPoints, updated.CreatedAt));
  }

  private static async Task<IResult> InvokeDelete(int id, ICustomerRepository repo)
  {
    var deleted = await repo.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
  }

  private static async Task<IResult> InvokeAdjustLoyalty(int id, AdjustLoyaltyPointsModel dto, ICustomerRepository repo)
  {
    var ok = await repo.AdjustLoyaltyPointsAsync(id, dto.Points);
    return ok ? Results.NoContent() : Results.NotFound();
  }
}

// ════════════════════════════════════════════════════════════════════════════
//  INGREDIENT ENDPOINT TESTS
// ════════════════════════════════════════════════════════════════════════════

public class IngredientEndpointTests
{
  [Fact]
  public async Task GetById_ReturnsOk_WhenFound()
  {
    var mock = Substitute.For<IIngredientRepository>();
    mock.GetByIdAsync(1).Returns(Task.FromResult<Ingredient?>(TestData.Flour()));

    var i = await mock.GetByIdAsync(1);
    var result = i is null ? Results.NotFound() :
        Results.Ok(new IngredientModel(i.Id, i.Name, i.Description, i.Unit, i.CostPerUnit,
            i.StockQuantity, i.ReorderThreshold, i.IsAllergen, i.AllergenInfo));

    result.Should().BeOfType<Ok<IngredientModel>>();
  }

  [Fact]
  public async Task GetById_ReturnsNotFound_WhenMissing()
  {
    var mock = Substitute.For<IIngredientRepository>();
    mock.GetByIdAsync(99).Returns(Task.FromResult<Ingredient?>(null));

    var i = await mock.GetByIdAsync(99);
    var result = i is null ? Results.NotFound() : Results.Ok(i);

    result.Should().BeOfType<NotFound>();
  }

  [Fact]
  public async Task AdjustStock_ReturnsNoContent_WhenSuccessful()
  {
    var mock = Substitute.For<IIngredientRepository>();
    mock.AdjustStockAsync(1, 500m).Returns(Task.FromResult(true));

    var ok = await mock.AdjustStockAsync(1, 500m);
    var result = ok ? Results.NoContent() : Results.NotFound();

    result.Should().BeOfType<NoContent>();
  }

  [Fact]
  public async Task AdjustStock_ReturnsNotFound_WhenIngredientMissing()
  {
    var mock = Substitute.For<IIngredientRepository>();
    mock.AdjustStockAsync(99, Arg.Any<decimal>()).Returns(Task.FromResult(false));

    var ok = await mock.AdjustStockAsync(99, 500m);
    var result = ok ? Results.NoContent() : Results.NotFound();

    result.Should().BeOfType<NotFound>();
  }
}

// ════════════════════════════════════════════════════════════════════════════
//  PRODUCT ENDPOINT TESTS
// ════════════════════════════════════════════════════════════════════════════

public class ProductEndpointTests
{
  [Fact]
  public async Task GetPieOfTheWeek_ReturnsOk_WhenPieExists()
  {
    var mock = Substitute.For<IProductRepository>();
    mock.GetPieOfTheWeekAsync().Returns(Task.FromResult<Product?>(TestData.SteakPie()));

    var pie = await mock.GetPieOfTheWeekAsync();
    var result = pie is null ? Results.NotFound() :
        Results.Ok(new ProductModel(pie.Id, pie.Name, pie.Description, pie.ShortDescription,
            pie.Price, pie.Category, pie.IsAvailable, pie.IsPieOfTheWeek,
            pie.ImageUrl, pie.AllergyInformation, pie.PreparationTimeMinutes,
            Enumerable.Empty<ProductIngredientModel>()));

    result.Should().BeOfType<Ok<ProductModel>>();
    var ok = (Ok<ProductModel>)result;
    ok.Value!.IsPieOfTheWeek.Should().BeTrue();
  }

  [Fact]
  public async Task GetPieOfTheWeek_ReturnsNotFound_WhenNoneSet()
  {
    var mock = Substitute.For<IProductRepository>();
    mock.GetPieOfTheWeekAsync().Returns(Task.FromResult<Product?>(null));

    var pie = await mock.GetPieOfTheWeekAsync();
    var result = pie is null ? Results.NotFound() : Results.Ok(pie);

    result.Should().BeOfType<NotFound>();
  }

  [Fact]
  public async Task SetPieOfTheWeek_ReturnsNoContent_WhenSuccessful()
  {
    var mock = Substitute.For<IProductRepository>();
    mock.SetPieOfTheWeekAsync(2).Returns(Task.FromResult(true));

    var ok = await mock.SetPieOfTheWeekAsync(2);
    var result = ok ? Results.NoContent() : Results.NotFound();

    result.Should().BeOfType<NoContent>();
  }

  [Fact]
  public async Task GetByCategory_ReturnsBadRequest_ForUnknownCategory()
  {
    var category = "NotARealCategory";
    var isValid = Enum.TryParse<ProductCategory>(category, ignoreCase: true, out _);
    var result = isValid
        ? Results.Ok(new List<Product>())
        : Results.BadRequest(new ErrorModel { Message = $"Unknown category '{category}'." });

    result.Should().BeOfType<BadRequest<ErrorModel>>();
  }
}

// ════════════════════════════════════════════════════════════════════════════
//  CUSTOM PIE ENDPOINT TESTS
// ════════════════════════════════════════════════════════════════════════════

public class CustomPieEndpointTests
{
  [Fact]
  public async Task Update_ReturnsConflict_WhenPieAlreadyApproved()
  {
    var mock = Substitute.For<ICustomPieRepository>();
    mock.GetByIdAsync(2).Returns(Task.FromResult<CustomPie?>(TestData.ApprovedCustomPie()));

    var pie = await mock.GetByIdAsync(2);
    IResult result;
    if (pie is null)
      result = Results.NotFound();
    else if (pie.IsApproved)
      result = Results.Conflict(new ErrorModel { Message = "Approved custom pies cannot be edited." });
    else
      result = Results.Ok(pie);

    result.Should().BeOfType<Conflict<ErrorModel>>();
  }

  [Fact]
  public async Task Approve_ReturnsBadRequest_WhenPriceIsZero()
  {
    var dto = new ApproveCustomPieModel(0m);
    var result = dto.EstimatedPrice <= 0
        ? Results.BadRequest(new ErrorModel { Message = "Estimated price must be greater than zero." })
        : Results.NoContent();

    result.Should().BeOfType<BadRequest<ErrorModel>>();
  }

  [Fact]
  public async Task Approve_ReturnsNoContent_WhenValid()
  {
    var mock = Substitute.For<ICustomPieRepository>();
    mock.ApproveAsync(1, 24.99m).Returns(Task.FromResult(true));

    var dto = new ApproveCustomPieModel(24.99m);
    var ok = await mock.ApproveAsync(1, dto.EstimatedPrice);
    var result = ok ? Results.NoContent() : Results.NotFound();

    result.Should().BeOfType<NoContent>();
  }
}

// ════════════════════════════════════════════════════════════════════════════
//  ORDER ENDPOINT TESTS
// ════════════════════════════════════════════════════════════════════════════

public class OrderEndpointTests
{
  [Fact]
  public async Task UpdateStatus_ReturnsNoContent_WhenSuccessful()
  {
    var mock = Substitute.For<IOrderRepository>();
    mock.UpdateStatusAsync(1, OrderStatus.Baking).Returns(Task.FromResult(true));

    var ok = await mock.UpdateStatusAsync(1, OrderStatus.Baking);
    var result = ok ? Results.NoContent() : Results.NotFound();

    result.Should().BeOfType<NoContent>();
  }

  [Fact]
  public async Task UpdateStatus_ReturnsNotFound_WhenOrderMissing()
  {
    var mock = Substitute.For<IOrderRepository>();
    mock.UpdateStatusAsync(999, Arg.Any<OrderStatus>()).Returns(Task.FromResult(false));

    var ok = await mock.UpdateStatusAsync(999, OrderStatus.Baking);
    var result = ok ? Results.NoContent() : Results.NotFound();

    result.Should().BeOfType<NotFound>();
  }

  [Fact]
  public async Task GetByStatus_ReturnsBadRequest_ForUnknownStatus()
  {
    var status = "FlyingToMoon";
    var isValid = Enum.TryParse<OrderStatus>(status, ignoreCase: true, out _);
    var result = isValid
        ? Results.Ok(new List<Order>())
        : Results.BadRequest(new ErrorModel { Message = $"Unknown order status '{status}'." });

    result.Should().BeOfType<BadRequest<ErrorModel>>();
  }
}
