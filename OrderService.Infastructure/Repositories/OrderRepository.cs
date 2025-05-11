using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entites;
using OrderService.Domain.Interfaces;
using OrderService.Infastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(OrderEntity order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderEntity>?> GetAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems) // Явно включаем связанные OrderProducts
                .ToListAsync();
            return orders;
        }

        public async Task<OrderEntity?> GetAsync(Guid id)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
