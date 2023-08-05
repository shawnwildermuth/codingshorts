using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MappingTest.Entities;
using MappingTest.Models;

namespace MappingTest;

public static class EntityExtensions
{
  public static Customer ToPoco(this CustomerModel cust)
  {
    return new Customer()
    {
      Id = cust.Id,
      CompanyName = cust.CompanyName ?? "",
      ContactName = cust.ContactName ?? "",
      Phone = cust.Phone,
      Address = new Address()
      { 
        Address1 = cust.Address1 ?? null,
        Address2 = cust.Address2 ?? null,
        Address3 = cust.Address3 ?? null,
        City = cust.City ?? "",
        StateProvince = cust.StateProvince ?? "",
        PostalCode = cust.PostalCode ?? "",
      },
      Orders = cust.Orders?.ToPocos()
    };
  }

  public static Order ToPoco(this OrderModel order)
  {
    return new Order()
    {
      Id = order.Id,
      OrderDate = order.OrderDate,
      CustomerId = order.CustomerId,
      Terms = order.Terms,
      OrderItems = order.OrderItems?.ToPocos()
    };
  }

  public static OrderItem ToPoco(this OrderItemModel item)
  {
    return new OrderItem()
    {
      Id = item.Id,
      Unit = item.Unit,
      UnitPrice = item.UnitPrice,
      ProductId = item.ProductId,
      Description = item.Description,
      Discount = item.Discount,
      Quantity = item.Quantity
    };
  }

  public static ICollection<Customer> ToPocos(this IEnumerable<CustomerModel> customers)
  {
    return customers.Select(c => c.ToPoco()).ToList();
  }

  public static ICollection<Order> ToPocos(this IEnumerable<OrderModel> orders)
  {
    return orders.Select(c => c.ToPoco()).ToList();
  }

  public static ICollection<OrderItem> ToPocos(this IEnumerable<OrderItemModel> items)
  {
    return items.Select(c => c.ToPoco()).ToList();
  }
}
