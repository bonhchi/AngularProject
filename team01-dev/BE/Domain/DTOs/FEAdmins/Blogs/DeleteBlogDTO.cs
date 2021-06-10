using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Blogs
{
    public class DeleteBlogDTO
    {
        public Guid Id { get; set; }
        
        public DateTime DeleteByDate { get; set; }
    }
}
