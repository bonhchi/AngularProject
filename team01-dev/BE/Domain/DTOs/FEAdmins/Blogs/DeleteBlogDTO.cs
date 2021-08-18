using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Blogs
{
    public class DeleteBlogDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
