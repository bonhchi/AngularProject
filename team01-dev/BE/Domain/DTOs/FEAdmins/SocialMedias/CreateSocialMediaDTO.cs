using Domain.DTOs.BaseDTOs;
using Domain.DTOs.Files;
using System.Collections.Generic;

namespace Domain.DTOs.SocialMedias
{
    public class CreateSocialMediaDTO : BaseCreateDTO
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string IconUrl { get; set; }

        public int DisplayOrder { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
