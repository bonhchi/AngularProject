using Domain.DTOs.Files;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Categories
{
    public class UpdateCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UpdatedByName { get; set; }
        public Guid UpdatedBy { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
