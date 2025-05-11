using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs.Response
{
    public class CheckStockQuantityResponse
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public int StockQuantity { get; set; }
    }
}
