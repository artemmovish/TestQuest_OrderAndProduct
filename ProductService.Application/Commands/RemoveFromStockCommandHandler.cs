using AutoMapper;
using MediatR;
using ProductService.Application.DTOs.Response;
using ProductService.Application.DTOs.Response.Base;
using ProductService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Commands
{
    public class RemoveFromStockCommandHandler : IRequestHandler<RemoveFromStockCommand, CommandResponse<StockQuantityResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public RemoveFromStockCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommandResponse<StockQuantityResponse>> Handle(RemoveFromStockCommand request, CancellationToken cancellationToken)
        {
            var stockQuantityRequest = request.stockQuantityRequest;
            var currentQuantity = await _repository.GetStockQuantityAsync(stockQuantityRequest.Id);

            if (currentQuantity < stockQuantityRequest.Quantity)
            {
                return new CommandResponse<StockQuantityResponse>(
                    false,
                    "Недостаточное количество товара",
                    new StockQuantityResponse
                    {
                        Id = stockQuantityRequest.Id,
                        StockQuantity = currentQuantity
                    });
            }

            await _repository.RemoveFromStockAsync(stockQuantityRequest.Id, stockQuantityRequest.Quantity);

            var updatedQuantity = await _repository.GetStockQuantityAsync(stockQuantityRequest.Id);

            return new CommandResponse<StockQuantityResponse>(
                true,
                "Количество товара успешно обновлено",
                new StockQuantityResponse
                {
                    Id = stockQuantityRequest.Id,
                    StockQuantity = updatedQuantity
                });
        }
    }
}
