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
    public class AddToStockProductCommandHandler : IRequestHandler<AddToStockProductCommand,
        CommandResponse<StockQuantityResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public AddToStockProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommandResponse<StockQuantityResponse>> Handle(AddToStockProductCommand request, CancellationToken cancellationToken)
        {
            var stockQuantityRequest = request.stockQuantityRequest;

            await _repository.AddToStockAsync(stockQuantityRequest.Id, stockQuantityRequest.Quantity);

            return new CommandResponse<StockQuantityResponse>(
                true,
                "Количество товара успешно обновлено",
                new StockQuantityResponse()
                {
                    Id = stockQuantityRequest.Id,
                    StockQuantity = await _repository.GetStockQuantityAsync(stockQuantityRequest.Id)
                });
        }
    }
}
