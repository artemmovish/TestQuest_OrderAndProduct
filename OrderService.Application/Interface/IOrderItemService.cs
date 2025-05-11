using OrderService.Application.DTOs.Requests;
using OrderService.Application.DTOs.Response;
using OrderService.Application.DTOs.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interface
{
    public interface IOrderItemService
    {
        Task<CommandResponse<OrderItemResponse>> GetOrderItemResponse(Guid id);
        Task<CommandResponse<CheckStockQuantityResponse>> CheckStockQuantity(Guid id, int quantity);
        Task<CommandResponse<OrderItemResponse>> RemoveFromStock(Guid id, int quantity);
    }
}
