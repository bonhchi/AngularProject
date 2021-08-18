﻿using Domain.DTOs.BaseDTOs;
using Domain.DTOs.Files;
using System.Collections.Generic;

namespace Domain.DTOs.Categories
{
    public class CreateCategoryDTO : BaseCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
