using MappingTest.Entities;
using MappingTest.Models;
using Mapster;

namespace MappingTest;

public class CustomerConfig : TypeAdapterConfig
{
  public CustomerConfig()
  {
    NewConfig<Customer, CustomerModel>()
      .TwoWays()
      .Map(c => c.Address1, src => src.Address!.Address1)
      .Map(c => c.Address2, src => src.Address!.Address2)
      .Map(c => c.Address3, src => src.Address!.Address3)
      .Map(c => c.City, src => src.Address!.City)
      .Map(c => c.StateProvince, src => src.Address!.StateProvince)
      .Map(c => c.PostalCode, src => src.Address!.PostalCode);

    NewConfig<OrderItem, OrderItemModel>()
//      .TwoWays()
      .Map(m => m.LineTotal, i => (i.Quantity * i.UnitPrice)*Convert.ToDecimal((1f-i.Discount)));
  }
}