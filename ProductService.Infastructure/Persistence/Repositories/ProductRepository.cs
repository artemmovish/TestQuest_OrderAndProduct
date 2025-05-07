using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductDbContext _context; 
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductEntity product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public Task<List<ProductEntity>> GetAsync()
        {
            var products = _context.Products.ToListAsync();
            return products;
        }

        public async Task<ProductEntity?> GetAsync(Guid id)
        {
            try
            {
                return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ошибка при получении продукта с ID: {id}", ex);
            }

        }

        public async Task<int> GetStockQuantityAsync(Guid id)
        {
            var product = await GetAsync(id);
            return product.StockQuantity;
        }
        public async Task AddToStockAsync(Guid id, int quantity)
        {
            var product = await GetAsync(id);
            product.StockQuantity += quantity;
            product.UpdateAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveFromStockAsync(Guid id, int quantity)
        {
            var product = await GetAsync(id);
            product.StockQuantity -= quantity;
            product.UpdateAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<(bool, int)> CheckStockQuantityAsync(Guid id, int quantity)
        {
            var product = await GetAsync(id);

            return (product.StockQuantity >= quantity, product.StockQuantity);
        }
    }
}
