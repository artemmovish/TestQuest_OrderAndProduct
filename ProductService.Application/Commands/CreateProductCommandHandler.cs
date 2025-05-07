using MediatR;
using ProductService.Application.DTOs.Response.Base;
using ProductService.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Domain.Interfaces;
using AutoMapper;
using ProductService.Domain.Entities;

namespace ProductService.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CommandResponse<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(request.Product);

            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;
            product.UpdateAt = DateTime.UtcNow;

            await _repository.AddAsync(product);

            return new CommandResponse<ProductResponse>(
                true,
                "Продукт создан успешно",
                _mapper.Map<ProductResponse>(product));
        }
    }
}
