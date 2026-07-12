namespace BakeAndCake.Api.Models;

public record CustomPieIngredientModel(int IngredientId, string IngredientName, decimal Quantity);

public record CustomPieModel(
    int Id,
    int CustomerId,
    string CustomerName,
    string Name,
    string? DedicationMessage,
    PieSize Size,
    PastryCrust CrustType,
    FillingType PrimaryFilling,
    string? SpecialInstructions,
    decimal EstimatedPrice,
    bool IsApproved,
    DateTime CreatedAt,
    DateTime? RequiredByDate,
    IEnumerable<CustomPieIngredientModel> Ingredients
);

public record CreateCustomPieModel(
    int CustomerId,
    string Name,
    string? DedicationMessage,
    PieSize Size,
    PastryCrust CrustType,
    FillingType PrimaryFilling,
    string? SpecialInstructions,
    DateTime? RequiredByDate,
    IEnumerable<CreateCustomPieIngredientModel> Ingredients
);

public record CreateCustomPieIngredientModel(int IngredientId, decimal Quantity);

public record UpdateCustomPieModel(
    string Name,
    string? DedicationMessage,
    PieSize Size,
    PastryCrust CrustType,
    FillingType PrimaryFilling,
    string? SpecialInstructions,
    DateTime? RequiredByDate,
    IEnumerable<CreateCustomPieIngredientModel> Ingredients
);

public record ApproveCustomPieModel(decimal EstimatedPrice);
