using System.Diagnostics.CodeAnalysis;

namespace BakeAndCake.Api.Data.Entities;

public class CustomPie
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  public Customer Customer { get; set; } = null!;
  public string Name { get; set; } = string.Empty; // e.g. "Mum's Birthday Pie"
  public string? DedicationMessage { get; set; } // written on the crust
  public PieSize Size { get; set; }
  public PastryCrust CrustType { get; set; }
  public FillingType PrimaryFilling { get; set; }
  public string? SpecialInstructions { get; set; }
  public decimal EstimatedPrice { get; set; }
  public bool IsApproved { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? RequiredByDate { get; set; }

  // Navigation
  public ICollection<CustomPieIngredient> CustomPieIngredients { get; set; } = [];
  public ICollection<OrderItem> OrderItems { get; set; } = [];
}