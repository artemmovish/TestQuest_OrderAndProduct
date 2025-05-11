using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderService.Application.DTOs.Response;
using OrderService.Application.DTOs.Response.Base;

namespace OrderService.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<CommandResponse<List<OrderResponse>>>
    {

    }
}
