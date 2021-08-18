using System;

namespace Domain.DTOs.CustomerWishList
{
    public class DeleteCustomerWishListDTO
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}
