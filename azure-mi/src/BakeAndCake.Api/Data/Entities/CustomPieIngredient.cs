namespace BakeAndCake.Api.Data.Entities;

/// <summary>Join table between CustomPie and Ingredient.</summary>
public class CustomPieIngredient
{
  public int CustomPieId { get; set; }
  public CustomPie CustomPie { get; set; } = null!;
  public int IngredientId { get; set; }
  public Ingredient Ingredient { get; set; } = null!;
  public decimal Quantity { get; set; }
}