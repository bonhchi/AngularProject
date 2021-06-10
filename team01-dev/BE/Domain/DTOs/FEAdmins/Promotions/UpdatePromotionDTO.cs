using Domain.DTOs.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Promotions
{
    public class UpdatePromotionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public decimal Value { get; set; } 
        public string ImageUrl { get; set; }
        public bool HasPercent { get; set; } 
        public List<FileDTO> Files { get; set; }
    }
}
