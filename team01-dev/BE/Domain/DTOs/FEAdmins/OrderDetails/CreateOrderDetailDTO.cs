using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.OrderDetails
{
    public class CreateOrderDetailDTO : BaseCreateDTO
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
