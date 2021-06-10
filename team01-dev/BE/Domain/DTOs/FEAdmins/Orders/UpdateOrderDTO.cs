using Domain.DTOs.Files;
using Domain.DTOs.OrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Orders
{
    public class UpdateOrderDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int TotalAmount { get; set; }
        public int TotalItem { get; set; }
        public string Note { get; set; }
        public List<CreateOrderDetailDTO> OrderDetails { get; set; }


    }
}
