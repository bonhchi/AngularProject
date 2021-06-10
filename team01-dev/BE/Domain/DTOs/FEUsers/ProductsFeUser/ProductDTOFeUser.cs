using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.ProductsFeUser
{
    public class ProductDTOFeUser : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; }

        public string ContentHTML { get; set; }

        public bool HasDisplayHomePage { get; set; }

        public int DisplayOrder { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal RatingScore { get; set; }
        public bool IsInWishList { get; set; }
    }
}
