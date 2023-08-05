using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Apis;
using Dealership.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dealership.Tests.UnitTests;

public class EmployeeApiTests
{
  [Fact]
  void CanGetEmployees()
  {
    var repo = new Repo();
    var result = EmployeeApi.GetEmployee(repo, 1);

    Assert.IsAssignableFrom<Ok<Employee>>(result);

    var value = ((Ok<Employee>)result).Value;

    Assert.NotNull(value);

    Assert.NotEmpty(value.FirstName!);
    Assert.NotEmpty(value.LastName!);

    Assert.True(value.Commission > 0.0);

  }
}
