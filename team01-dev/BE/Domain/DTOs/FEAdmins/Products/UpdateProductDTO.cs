using Domain.DTOs.BaseDTOs;
using Domain.DTOs.Files;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Products
{
    public class UpdateProductDTO : BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; }

        public string ContentHTML { get; set; }

        public bool HasDisplayHomePage { get; set; }

        public int DisplayOrder { get; set; }
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SaleCount { get; set; }

        public List<FileDTO> Files { get; set; }
    }
}
