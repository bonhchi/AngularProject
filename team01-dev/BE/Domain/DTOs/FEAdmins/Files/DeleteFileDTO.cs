using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Files
{
    public class DeleteFileDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
