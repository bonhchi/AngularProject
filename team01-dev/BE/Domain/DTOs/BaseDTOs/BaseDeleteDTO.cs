using System;

namespace Domain.DTOs.BaseDTOs
{
    public class BaseDeleteDTO
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedByName { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime DeleteByDate { get; set; }
    }
}
