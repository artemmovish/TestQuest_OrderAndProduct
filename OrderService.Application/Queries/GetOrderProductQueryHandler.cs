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

namespace OrderService.Application.Queries
{
    public class GetOrderProductQueryHandler : IRequestHandler<GetOrderProductQuery, CommandResponse<OrderResponse>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetOrderProductQueryHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommandResponse<OrderResponse>> Handle(GetOrderProductQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAsync(request.Id);

            if (response == null)
            {
                return new CommandResponse<OrderResponse>(
                false,
                "Товар не найден",
                null);
            }

            return new CommandResponse<OrderResponse>(
                true,
                "Товар успешно получен",
                _mapper.Map<OrderResponse>(response));
        }
    }
}
