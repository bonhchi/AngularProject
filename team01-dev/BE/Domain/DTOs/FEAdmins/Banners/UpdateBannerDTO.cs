using Domain.DTOs.Files;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Banners
{
    public class UpdateBannerDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
