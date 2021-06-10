using Domain.DTOs.BaseDTOs;
using Domain.DTOs.OrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Orders
{
    public class OrderDTO : BaseDTO
    {
        public string FullName { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int TotalAmount { get; set; }
        public int TotalItem { get; set; }
        public string Note { get; set; }
        public Guid CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponPercent { get; set; }
        public decimal CouponValue { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
