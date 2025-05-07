using MediatR;
using ProductService.Application.DTOs.Response.Base;
using ProductService.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Queries
{
    public class CheckStockQuantityQueryHandler : IRequestHandler<CheckStockQuantityQuery,
        CommandResponse<CheckStockQuantityResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CheckStockQuantityQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommandResponse<CheckStockQuantityResponse>> Handle(CheckStockQuantityQuery request, CancellationToken cancellationToken)
        {
            var stockQuantityRequest = request.stockQuantityRequest;

            var product = await _repository.GetAsync(stockQuantityRequest.Id);

            if (product == null)
            {
                return new CommandResponse<CheckStockQuantityResponse>(
                false,
                "Товар не найден",
                null);
            }

            return new CommandResponse<CheckStockQuantityResponse>(
                true,
                "Товар найден",
                new CheckStockQuantityResponse()
                {
                    Id = product.Id,
                    Success = (product.StockQuantity >= stockQuantityRequest.Quantity),
                    StockQuantity = product.StockQuantity
                });
        }
    }
}
