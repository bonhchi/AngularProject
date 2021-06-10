using Domain.DTOs.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Blogs
{
    public class UpdateBlogDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string ContentHTML { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UpdateByDate { get; set; }
        public string CreatedByName { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
