using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.OrderDetails
{
    public class OrderDetailDTO : BaseDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
    }
}
