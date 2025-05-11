using MediatR;
using OrderService.Application.DTOs.Response;
using OrderService.Application.DTOs.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries
{
    public class GetOrderProductQuery : IRequest<CommandResponse<OrderResponse>>
    {
        public Guid Id { get; set; }
    }
}
