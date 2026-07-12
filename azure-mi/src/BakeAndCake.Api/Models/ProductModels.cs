namespace BakeAndCake.Api.Models;

public record ProductIngredientModel(int IngredientId, string IngredientName, decimal QuantityRequired);

public record ProductModel(
    int Id,
    string Name,
    string? Description,
    string? ShortDescription,
    decimal Price,
    ProductCategory Category,
    bool IsAvailable,
    bool IsPieOfTheWeek,
    string? ImageUrl,
    string? AllergyInformation,
    int PreparationTimeMinutes,
    IEnumerable<ProductIngredientModel> Ingredients
);

public record CreateProductModel(
    string Name,
    string? Description,
    string? ShortDescription,
    decimal Price,
    ProductCategory Category,
    bool IsAvailable,
    bool IsPieOfTheWeek,
    string? ImageUrl,
    string? AllergyInformation,
    int PreparationTimeMinutes,
    IEnumerable<CreateProductIngredientModel> Ingredients
);

public record CreateProductIngredientModel(int IngredientId, decimal QuantityRequired);

public record UpdateProductModel(
    string Name,
    string? Description,
    string? ShortDescription,
    decimal Price,
    ProductCategory Category,
    bool IsAvailable,
    bool IsPieOfTheWeek,
    string? ImageUrl,
    string? AllergyInformation,
    int PreparationTimeMinutes,
    IEnumerable<CreateProductIngredientModel> Ingredients
);
