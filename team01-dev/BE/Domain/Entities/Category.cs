using Domain.DTOs.Categories;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; }

        public override void Insert()
        {
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateCategoryDTO model)
        {
            base.Update();
            Name = model.Name;
            Description = model.Description;
            ImageUrl = model.ImageUrl;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }
    }
}
