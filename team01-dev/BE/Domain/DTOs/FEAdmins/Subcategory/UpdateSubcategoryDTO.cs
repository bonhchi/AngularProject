using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.FEAdmins.Subcategory
{
    public class UpdateSubcategoryDTO : BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryTypeId { get; set; }
    }
}
