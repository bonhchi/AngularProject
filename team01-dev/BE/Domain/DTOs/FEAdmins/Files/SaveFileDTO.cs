using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs.Files
{
    public class SaveFileDTO
    {
        [Required]
        public String EntityType { get; set; }
        public String EntityId { get; set; }
        public int TypeUpload { get; set; } = 1;
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
