using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoeMoney.Data.Entities;

namespace ShoeMoney;
public record OrderCreated(Order Order);
public record OrderSubmitted(Order Order);

