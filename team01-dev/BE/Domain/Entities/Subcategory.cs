using Domain.DTOs.FEAdmins.Subcategory;
using System;

namespace Domain.Entities
{
    public class Subcategory : BaseEntity
    {
        public string Name { get; set; }

        public Guid SubcategoryTypeId { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual SubcategoryType SubcategoryType { get; set; }

        public override void Insert()
        {
            base.Insert();
        }

        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateSubcategoryDTO model)
        {
            base.Update();
            Name = model.Name;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

    }
}
