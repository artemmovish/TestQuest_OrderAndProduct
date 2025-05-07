using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.DTOs.Requests
{
    public class StockQuantityRequest
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
