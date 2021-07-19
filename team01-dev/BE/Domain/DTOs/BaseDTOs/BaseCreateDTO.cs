using System;

namespace Domain.DTOs.BaseDTOs
{
    public class BaseCreateDTO
    {
        public string CreatedByName { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreateByDate { get; set; }
    }
}
