using Domain.DTOs.BaseDTOs;
using Domain.DTOs.CustomerWishList;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Products
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; }

        public string ContentHTML { get; set; }

        public bool HasDisplayHomePage { get; set; }
        public decimal RatingScore { get; set; }
        public int DisplayOrder { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int SaleCount { get; set; }
        public bool IsInWishList { get; set; }
        public List<CustomerWishListDTO> CustomerWishLists { get; set; }
        
    }
}
