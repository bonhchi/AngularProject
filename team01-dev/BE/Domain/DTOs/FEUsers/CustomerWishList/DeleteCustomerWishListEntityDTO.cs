using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.CustomerWishList
{
    public class DeleteCustomerWishListEntityDTO
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}
