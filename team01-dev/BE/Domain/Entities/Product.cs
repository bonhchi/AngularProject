using Domain.DTOs.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public string ContentHTML { get; set; }
        public bool HasDisplayHomePage { get; set; }
        public int DisplayOrder { get; set; }
        public int SaleCount { get; set; }
        public Guid CategoryId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RatingScore { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<CustomerWishList> CustomerWishLists { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }

        public override void Insert()
        {
            RatingScore = 5;
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateProductDTO model)
        {
            base.Update();
            Name = model.Name;
            Description = model.Description;
            IsFeatured = model.IsFeatured;
            ContentHTML = model.ContentHTML;
            HasDisplayHomePage = model.HasDisplayHomePage;
            DisplayOrder = model.DisplayOrder;
            CategoryId = model.CategoryId;
            ImageUrl = model.ImageUrl;
            Price = model.Price;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public void UpdateRating(decimal ratingScore)
        {
            RatingScore = ratingScore;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }
    }
}
