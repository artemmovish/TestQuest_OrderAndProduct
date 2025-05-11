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
            var currentQuantity = await _repository.GetStockQuantityAsync(request.Id);

            if (currentQuantity < request.Quantity)
            {
                return new CommandResponse<StockQuantityResponse>(
                    false,
                    "Недостаточное количество товара",
                    new StockQuantityResponse
                    {
                        Id = request.Id,
                        StockQuantity = currentQuantity
                    });
            }

            await _repository.RemoveFromStockAsync(request.Id, request.Quantity);

            var updatedQuantity = await _repository.GetStockQuantityAsync(request.Id);

            return new CommandResponse<StockQuantityResponse>(
                true,
                "Количество товара успешно обновлено",
                new StockQuantityResponse
                {
                    Id = request.Id,
                    StockQuantity = updatedQuantity
                });
        }
    }
}
