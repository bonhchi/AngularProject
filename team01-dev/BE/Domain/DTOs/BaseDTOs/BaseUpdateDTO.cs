using System;

namespace Domain.DTOs.BaseDTOs
{
    public class BaseUpdateDTO
    {
        public string UpdatedByName { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdateByDate { get; set; }
    }
}
