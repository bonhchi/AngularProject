using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.FEAdmins.Subcategory
{
    public class CreateSubcategoryDTO: BaseCreateDTO
    {
        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SubcategoryTypeId { get; set; }
    }
}
