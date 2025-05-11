using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs.Response
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public ICollection<OrderItemResponse> OrderItems { get; set; }
    }
}
