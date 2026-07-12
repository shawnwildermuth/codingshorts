using Mapster;

namespace BakeAndCake.Api.Data.Mapping;

public class MappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    // ProductIngredients collection has a different name from Ingredients
    // and requires flattening Ingredient.Name into IngredientName.
    config.NewConfig<Product, ProductModel>()
        .Map(dest => dest.Ingredients,
             src => src.ProductIngredients.Select(pi => new ProductIngredientModel(
                 pi.IngredientId,
                 pi.Ingredient.Name,
                 pi.QuantityRequired)));

    config.NewConfig<CreateProductModel, Product>()
        .Map(dest => dest.ProductIngredients,
             src => src.Ingredients.Select(i => new ProductIngredient
             {
               IngredientId = i.IngredientId,
               QuantityRequired = i.QuantityRequired
             }).ToList());

    config.NewConfig<UpdateProductModel, Product>()
        .Map(dest => dest.ProductIngredients,
             src => src.Ingredients.Select(i => new ProductIngredient
             {
               IngredientId = i.IngredientId,
               QuantityRequired = i.QuantityRequired
             }).ToList())
        .Ignore(dest => dest.Id);

    // CustomerName is a computed field; CustomPieIngredients differs from Ingredients.
    config.NewConfig<CustomPie, CustomPieModel>()
        .Map(dest => dest.CustomerName,
             src => $"{src.Customer.FirstName} {src.Customer.LastName}")
        .Map(dest => dest.Ingredients,
             src => src.CustomPieIngredients.Select(cpi => new CustomPieIngredientModel(
                 cpi.IngredientId,
                 cpi.Ingredient.Name,
                 cpi.Quantity)));

    config.NewConfig<CreateCustomPieModel, CustomPie>()
        .Map(dest => dest.CustomPieIngredients,
             src => src.Ingredients.Select(i => new CustomPieIngredient
             {
               IngredientId = i.IngredientId,
               Quantity = i.Quantity
             }).ToList());

    config.NewConfig<UpdateCustomPieModel, CustomPie>()
        .Map(dest => dest.CustomPieIngredients,
             src => src.Ingredients.Select(i => new CustomPieIngredient
             {
               IngredientId = i.IngredientId,
               Quantity = i.Quantity
             }).ToList())
        .Ignore(dest => dest.Id);

    // ProductName and CustomPieName are flattened from navigation properties.
    config.NewConfig<OrderItem, OrderItemModel>()
        .Map(dest => dest.ProductName, src => src.Product != null ? src.Product.Name : null)
        .Map(dest => dest.CustomPieName, src => src.CustomPie != null ? src.CustomPie.Name : null);

    // CustomerName is computed; OrderItems maps to Items (different name).
    config.NewConfig<Order, OrderModel>()
        .Map(dest => dest.CustomerName,
             src => src.Customer != null
                 ? $"{src.Customer.FirstName} {src.Customer.LastName}"
                 : null)
        .Map(dest => dest.Items, src => src.OrderItems);

    // OrderTotal is sourced from the Order navigation property.
    config.NewConfig<Receipt, ReceiptModel>()
        .Map(dest => dest.OrderTotal,
             src => src.Order.TotalAmount);
  }
}
