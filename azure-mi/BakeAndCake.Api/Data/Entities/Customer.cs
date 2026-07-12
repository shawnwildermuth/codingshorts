namespace BakeAndCake.Api.Data.Entities;

public class Customer
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string? Phone { get; set; }
  public string? Address { get; set; }
  public bool IsLoyaltyMember { get; set; }
  public int LoyaltyPoints { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  // Navigation
  public ICollection<Order> Orders { get; set; } = [];
  public ICollection<CustomPie> CustomPies { get; set; } = [];
}