using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.FEAdmins.Subcategory
{
    public class SubcategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public Guid SubcategoryTypeId { get; set; }
        public Guid CategoryId { get; set; }
        public string SubcategoryTypeName { get; set; }
        public string CategoryName { get; set; }
    }
}
