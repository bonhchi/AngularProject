using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Files
{
    public class SaveFileDTO
    {
        [Required]
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public int TypeUpload { get; set; } = 1;
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
