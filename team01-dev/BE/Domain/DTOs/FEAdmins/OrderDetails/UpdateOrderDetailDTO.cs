using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.OrderDetails
{
    public class UpdateOrderDetailDTO: BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
    }
}
