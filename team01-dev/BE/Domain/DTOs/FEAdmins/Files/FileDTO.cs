using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Files
{
    public class FileDTO : BaseDTO
    {
        public String Name { get; set; }
        public String Url { get; set; }
        public String FileExt { get; set; }
        public String EntityType { get; set; }
        public String EntityId { get; set; }
        public int TypeUpload { get; set; }
    }
}
