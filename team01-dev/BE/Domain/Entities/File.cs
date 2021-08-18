using System;

namespace Domain.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string FileExt { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
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
