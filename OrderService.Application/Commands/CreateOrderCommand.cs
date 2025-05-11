using MediatR;
using OrderService.Application.DTOs.Requests;
using OrderService.Application.DTOs.Response;
using OrderService.Application.DTOs.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class CreateOrderCommand : IRequest<CommandResponse<OrderResponse>>
    {
        public OrderCreateRequest OrderCreateRequest { get; set; }
    }
}
