using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class File : BaseEntity
    {
        public String Name { get; set; }
        public String Url { get; set; }
        public String FileExt { get; set; }
        public String EntityType { get; set; }
        public String EntityId { get; set; }
        public int TypeUpload { get; set; }

        public override void Insert()
        {
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
