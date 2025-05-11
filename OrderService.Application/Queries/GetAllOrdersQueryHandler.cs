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

namespace OrderService.Application.Queries
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, CommandResponse<List<OrderResponse>>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResponse<List<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAsync();
            return new CommandResponse<List<OrderResponse>>(
                true,
                "Товары получены",
                _mapper.Map<List<OrderResponse>>(response));
        }
    }
}
