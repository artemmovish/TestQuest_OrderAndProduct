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

namespace ProductService.Application.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, CommandResponse<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommandResponse<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var Id = request.Id;

            var product = await _repository.GetAsync(Id);

            if (product == null)
            {
                return new CommandResponse<ProductResponse>(
                false,
                "Товар не найден",
                null);
            }

            return new CommandResponse<ProductResponse>(
                true,
                "Товар успешно получен",
                _mapper.Map<ProductResponse>(product));
        }
    }
}
