using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoeMoney.Enums;

namespace ShoeMoney.Data.Entities;
public  class Payment
{
  public int Id { get; set; }
  public PaymentType PaymentType { get; set; }
  public int OrderId { get; set; }
  public decimal Amount { get; set; }
  public string? CardNumber { get; set; }
  public string? Cvv { get; set; }
  public string? Expiration { get; set; }

}
