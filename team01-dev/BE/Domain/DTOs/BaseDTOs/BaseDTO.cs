using System;

namespace Domain.DTOs.BaseDTOs
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByName { get; set; }
        public Guid CreatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public Guid UpdatedBy { get; set; }
        public string DeletedByName { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime CreateByDate { get; set; }
        public DateTime UpdateByDate { get; set; }
        public DateTime DeleteByDate { get; set; }
    }
}
