using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrderService.Application.DTOs.Requests;
using OrderService.Application.DTOs.Response;
using OrderService.Domain.Entites;

namespace OrderService.Application.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateRequest, OrderEntity>();
            CreateMap<OrderEntity, OrderResponse>();
            CreateMap<OrderItemRequest, OrderItemEntity>();
            CreateMap<OrderItemEntity, OrderItemResponse>();
        }
    }
}
