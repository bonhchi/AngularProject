using System;

namespace Domain.DTOs.Files
{
    public class UpdateFileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string FileExt { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public int TypeUpload { get; set; }
    }
}
