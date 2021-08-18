using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.OrderDetails
{
    public class DeleteOrderDetailDTO : BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
