using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Orders
{
    public class DeleteOrderDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
