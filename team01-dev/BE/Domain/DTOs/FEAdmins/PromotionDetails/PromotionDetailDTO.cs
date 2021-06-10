using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.PromotionDetails
{
    public class PromotionDetailDTO : BaseDTO
    {
        public Guid PromotionID { get; set; }
        public Guid ProductId { get; set; } 
        public decimal PriceSale { get; set; } 
        public decimal ValuePercent { get; set; }
        public decimal Value { get; set; }  
    }
}
