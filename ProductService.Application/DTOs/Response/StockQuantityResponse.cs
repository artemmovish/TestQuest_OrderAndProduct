using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.DTOs.Response
{
    public class StockQuantityResponse
    {
        public Guid Id { get; set; }
        public int StockQuantity { get; set; }
    }
}
