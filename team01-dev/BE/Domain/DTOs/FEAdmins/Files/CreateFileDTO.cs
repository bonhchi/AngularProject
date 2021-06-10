using System;

namespace Domain.DTOs.Files
{
    public class CreateFileDTO
    {
        public String Name { get; set; }
        public String Url { get; set; }
        public String FileExt { get; set; }
        public String EntityType { get; set; }
        public String EntityId { get; set; }
        public int TypeUpload { get; set; }
    }
}
