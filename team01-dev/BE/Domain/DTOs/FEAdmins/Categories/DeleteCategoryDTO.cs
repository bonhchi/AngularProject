using System;

namespace Domain.DTOs.Categories
{
    public class DeleteCategoryDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedByName { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
