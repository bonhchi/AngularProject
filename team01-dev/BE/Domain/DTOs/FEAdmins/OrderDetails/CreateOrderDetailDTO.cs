using Domain.DTOs.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.OrderDetails
{
    public class CreateOrderDetailDTO
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
