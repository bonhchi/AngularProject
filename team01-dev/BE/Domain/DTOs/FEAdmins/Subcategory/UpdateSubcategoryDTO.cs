using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.FEAdmins.Subcategory
{
    public class UpdateSubcategoryDTO : BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
