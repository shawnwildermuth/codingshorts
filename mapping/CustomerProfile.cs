using AutoMapper;
using MappingTest.Entities;
using MappingTest.Models;

namespace MappingTest;

public class CustomerProfile : Profile
{
  public CustomerProfile()
  {
    CreateMap<Customer, CustomerModel>()
      .ForMember(c => c.Address1, opt => opt.MapFrom(m => m.Address!.Address1))
      .ForMember(c => c.Address2, opt => opt.MapFrom(m => m.Address!.Address2))
      .ForMember(c => c.Address3, opt => opt.MapFrom(m => m.Address!.Address3))
      .ForMember(c => c.City, opt => opt.MapFrom(m => m.Address!.City))
      .ForMember(c => c.StateProvince, opt => opt.MapFrom(m => m.Address!.StateProvince))
      .ForMember(c => c.PostalCode, opt => opt.MapFrom(m => m.Address!.PostalCode))
      .ReverseMap();
    CreateMap<Order, OrderModel>()
      .ReverseMap();
    CreateMap<OrderItem, OrderItemModel>()
      .ForMember(i => i.LineTotal, opt => opt.MapFrom(i => (i.Quantity * i.UnitPrice)*Convert.ToDecimal((1f-i.Discount))))
      .ReverseMap();
  }
}