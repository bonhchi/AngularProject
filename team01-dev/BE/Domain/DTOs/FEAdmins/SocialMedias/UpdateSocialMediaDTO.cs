using Domain.DTOs.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.SocialMedias
{
    public class UpdateSocialMediaDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Link { get; set; }

        public string IconUrl { get; set; }

        public int DisplayOrder { get; set; }

        public List<FileDTO> Files { get; set; }

    }
}
