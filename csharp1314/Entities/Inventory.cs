namespace CSharpNew.Entities;

public class Inventory
{
  public int OnHand { get; set; }
  public int OnOrder { get; set; }
  public int OnBackorder { get; set; }
  public Location? Store { get; set; }
}