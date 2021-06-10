using Microsoft.AspNetCore.Http;
using System;

namespace Domain.DTOs.Files
{
    public class UpdateFileDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String FileExt { get; set; }
        public String EntityType { get; set; }
        public String EntityId { get; set; }
        public int TypeUpload { get; set; }
    }
}
