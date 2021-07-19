using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Products
{
    public class DeleteProductDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
