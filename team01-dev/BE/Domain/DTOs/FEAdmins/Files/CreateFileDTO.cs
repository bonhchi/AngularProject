using Domain.DTOs.BaseDTOs;

namespace Domain.DTOs.Files
{
    public class CreateFileDTO: BaseCreateDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string FileExt { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public int TypeUpload { get; set; }
    }
}
