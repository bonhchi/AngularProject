using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Customer
{
    public class DeleteCustomerDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
