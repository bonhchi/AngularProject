using Domain.DTOs.BaseDTOs;
using Domain.DTOs.Files;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Blogs
{
    public class UpdateBlogDTO : BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string ContentHTML { get; set; }
        public string ImageUrl { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
