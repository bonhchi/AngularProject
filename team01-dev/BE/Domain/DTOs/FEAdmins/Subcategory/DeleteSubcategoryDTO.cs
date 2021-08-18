using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.FEAdmins.Subcategory
{
    public class DeleteSubcategoryDTO : BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
