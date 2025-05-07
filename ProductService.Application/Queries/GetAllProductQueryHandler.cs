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
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, CommandResponse<List<ProductResponse>>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommandResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAsync();

            return new CommandResponse<List<ProductResponse>>(
                true,
                "Продукты получены",
                _mapper.Map<List<ProductResponse>>(products));
        }
    }
}
