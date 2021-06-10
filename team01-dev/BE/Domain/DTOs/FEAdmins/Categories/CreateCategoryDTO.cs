using Domain.DTOs.Files;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Categories
{
    public class CreateCategoryDTO 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
