using OrderService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<OrderEntity>?> GetAsync();
        public Task<OrderEntity?> GetAsync(Guid id);
        public Task CreateAsync(OrderEntity order);
    }
}
