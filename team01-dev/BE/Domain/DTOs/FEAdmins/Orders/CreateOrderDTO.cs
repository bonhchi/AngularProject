using Domain.DTOs.Files;
using Domain.DTOs.OrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Orders
{
    public class CreateOrderDTO
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItem { get; set; }
        public Guid CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponPercent { get; set; }
        public decimal CouponValue { get; set; }

        public List<CreateOrderDetailDTO> OrderDetails{ get; set; }
    }
}
