using Domain.DTOs.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment: BaseEntity
    {
        public string FullName { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public override void Insert()
        {
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }
        
    }
}
