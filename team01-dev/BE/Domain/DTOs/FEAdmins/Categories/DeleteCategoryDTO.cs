using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Categories
{
    public class DeleteCategoryDTO : BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
