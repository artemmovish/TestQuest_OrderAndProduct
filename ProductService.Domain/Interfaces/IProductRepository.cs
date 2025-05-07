using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAsync();
        Task<ProductEntity?> GetAsync(Guid id);
        Task AddAsync(ProductEntity product);
        Task<int> GetStockQuantityAsync(Guid id);
        Task<(bool, int)> CheckStockQuantityAsync(Guid id, int quantity);
        Task AddToStockAsync(Guid id, int quantity);
        Task RemoveFromStockAsync(Guid id, int quantity);
    }
}
