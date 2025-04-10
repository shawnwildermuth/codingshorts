using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMoney.Data.Entities;

public class OrderItem
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public Product? Product { get; set; }
  public string? Size { get; set; }
  public string? Width { get; set; }
  public decimal Price { get; set; }
  public decimal Quantity { get; set; }
  public float Discount { get; set; }
  public int OrderId { get; set; }
}
