using MediatR;
using ProductService.Application.DTOs.Requests;
using ProductService.Application.DTOs.Response;
using ProductService.Application.DTOs.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Queries
{
    public class CheckStockQuantityQuery : IRequest<CommandResponse<CheckStockQuantityResponse>>
    {
        public Guid Id { get; set; }
        public int StockQuantity { get; set; }
    }
}
