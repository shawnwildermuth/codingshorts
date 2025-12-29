using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoeMoney.Data.Entities;

namespace ShoeMoney.Models;
public record OrderCreated(Order Order) {}
public record OrderSaved(Order Order) { }
public record OrderError(Order Order, string ErrorMessage);