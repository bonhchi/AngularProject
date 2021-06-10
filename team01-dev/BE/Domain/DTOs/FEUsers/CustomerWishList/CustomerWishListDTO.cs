using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.CustomerWishList
{
    public class CustomerWishListDTO: BaseDTO
    {
        public Guid CustomerId { get; set; }
        public Guid EntityId { get; set; }
    }
}
