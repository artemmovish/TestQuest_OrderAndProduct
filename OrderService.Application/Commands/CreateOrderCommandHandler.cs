using MediatR;
using OrderService.Application.DTOs.Response.Base;
using OrderService.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrderService.Domain.Interfaces;
using OrderService.Application.Interface;
using OrderService.Domain.Entites;

namespace OrderService.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CommandResponse<OrderResponse>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOrderItemService _service;
        public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper, IOrderItemService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CommandResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = request.OrderCreateRequest;

            // 1. Check stock for all items first
            foreach (var item in order.OrderItems)
            {
                var serviceResponse = await _service.CheckStockQuantity(item.ProductId, item.Quantity);

                if (!serviceResponse.Success || !serviceResponse.Data.Success)
                {
                    return new CommandResponse<OrderResponse>(
                        false,
                        $"Продукт с ID {serviceResponse.Data.Id} " +
                        $"не имеет нужное количество товара: {serviceResponse.Data.StockQuantity} из {item.Quantity}",
                        null);
                }
            }

            // 2. Remove items from stock
            foreach (var item in order.OrderItems)
            {
                var removeResponse = await _service.RemoveFromStock(item.ProductId, item.Quantity);

                if (!removeResponse.Success)
                {
                    return new CommandResponse<OrderResponse>(
                        false,
                        $"Не удалось зарезервировать продукт с ID {item.ProductId}: {removeResponse.Message}",
                        null);
                }
            }

            // 3. Create the order
            var orderEntity = _mapper.Map<OrderEntity>(order);
            orderEntity.Id = Guid.NewGuid();
            orderEntity.CreatedAt = DateTime.UtcNow;

            foreach (var item in orderEntity.OrderItems)
            {
                item.Id = Guid.NewGuid();
                item.OrderId = orderEntity.Id;
            }

            try
            {
                await _repository.CreateAsync(orderEntity);

                return new CommandResponse<OrderResponse>(
                    true,
                    "Заказ оформлен успешно",
                    _mapper.Map<OrderResponse>(orderEntity));
            }
            catch (Exception ex)
            {
                // Consider compensating transaction for stock removal here
                return new CommandResponse<OrderResponse>(
                    false,
                    $"Ошибка при создании заказа: {ex.Message}",
                    null);
            }
        }
    }
}
