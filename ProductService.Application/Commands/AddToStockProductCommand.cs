using MediatR;
using ProductService.Application.DTOs.Requests;
using ProductService.Application.DTOs.Response;
using ProductService.Application.DTOs.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Commands
{
    public class AddToStockProductCommand : IRequest<CommandResponse<StockQuantityResponse>>
    {
        public StockQuantityRequest stockQuantityRequest { get;set;}
    }
}
