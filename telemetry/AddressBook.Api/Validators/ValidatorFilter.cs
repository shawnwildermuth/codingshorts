using FluentValidation;

namespace AddressBook.Api.Validators;

public class ValidationFilter<TModel> : IEndpointFilter
    where TModel : class
{
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
      EndpointFilterDelegate next)
  {
    // Find argument of Type
    var modelArgument = context.Arguments.Where(a => a?.GetType() == typeof(TModel)).First() as TModel;

    if (modelArgument is not null)
    {
      // Find the validator (will throw exception, bu thtat is ok
      var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<TModel>>();

      // Test the validation
      var result = await validator.ValidateAsync(modelArgument);
      if (result.IsValid)
      {
        // Continue middleware
        return await next(context);
      }

      var validationErrors = result.Errors
          .ToDictionary(e => e.PropertyName, e => new[] { e.ErrorMessage });

      return Results.ValidationProblem(validationErrors, $"{nameof(TModel)} failed validation");
    }

    return Results.Problem("Could not find object to validate.");
  }
}

public static class ValidatorFilterExtensions
{
  public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder filter)
    where T : class
  {
    filter.AddEndpointFilter<ValidationFilter<T>>();
    return filter;
  }
}

