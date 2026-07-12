namespace BakeAndCake.Api.Models;

public record IngredientModel(
    int Id,
    string Name,
    string? Description,
    string Unit,
    decimal CostPerUnit,
    decimal StockQuantity,
    decimal ReorderThreshold,
    bool IsAllergen,
    string? AllergenInfo
);

public record CreateIngredientModel(
    string Name,
    string? Description,
    string Unit,
    decimal CostPerUnit,
    decimal StockQuantity,
    decimal ReorderThreshold,
    bool IsAllergen,
    string? AllergenInfo
);

public record UpdateIngredientModel(
    string Name,
    string? Description,
    string Unit,
    decimal CostPerUnit,
    decimal StockQuantity,
    decimal ReorderThreshold,
    bool IsAllergen,
    string? AllergenInfo
);

public record AdjustStockModel(decimal Quantity);  // positive = restock, negative = use
