namespace BakeAndCake.Api.Data.Entities;

/// <summary>Join table between Product and Ingredient.</summary>
public class ProductIngredient
{
  public int ProductId { get; set; }
  public Product Product { get; set; } = null!;
  public int IngredientId { get; set; }
  public Ingredient Ingredient { get; set; } = null!;
  public decimal QuantityRequired { get; set; }
}