using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Comments
{
    public class UpdateCommentDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}
