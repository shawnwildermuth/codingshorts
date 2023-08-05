using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MappingTest.Entities;
using MappingTest.Models;

namespace MappingTest;

public static class CustomerExtensions
{
  public static CustomerModel ToModel(this Customer cust)
  {
    return new CustomerModel()
    {
      Id = cust.Id,
      CompanyName = cust.CompanyName ?? "",
      ContactName = cust.ContactName ?? "",
      Phone = cust.Phone,
      Address1 = cust.Address?.Address1 ?? "",
      Address2 = cust.Address?.Address2,
      Address3 = cust.Address?.Address3,
      City = cust.Address?.City ?? "",
      StateProvince = cust.Address?.StateProvince ?? "",
      PostalCode = cust.Address?.PostalCode ?? "",
      Orders = cust.Orders?.ToModels()
    };
  }

  public static OrderModel ToModel(this Order order)
  {
    return new OrderModel()
    {
      Id = order.Id,
      OrderDate = order.OrderDate,
      CustomerId = order.CustomerId,
      Terms = order.Terms,
      OrderItems = order.OrderItems?.ToModels()
    };
  }

  public static OrderItemModel ToModel(this OrderItem item)
  {
    return new OrderItemModel()
    {
      Id = item.Id,
      Unit = item.Unit,
      UnitPrice = item.UnitPrice,
      ProductId = item.ProductId,
      Description = item.Description,
      Discount = item.Discount,
      Quantity = item.Quantity,
      LineTotal = (item.UnitPrice * item.Quantity) * (1m - (decimal)item.Discount)
    };
  }

  public static ICollection<CustomerModel> ToModels(this IEnumerable<Customer> customers)
  {
    return customers.Select(c => c.ToModel()).ToList();
  }

  public static ICollection<OrderModel> ToModels(this IEnumerable<Order> orders)
  {
    return orders.Select(c => c.ToModel()).ToList();
  }

  public static ICollection<OrderItemModel> ToModels(this IEnumerable<OrderItem> items)
  {
    return items.Select(c => c.ToModel()).ToList();
  }
}
