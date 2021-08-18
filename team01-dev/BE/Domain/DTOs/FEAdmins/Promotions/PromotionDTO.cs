﻿using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Promotions
{
    public class PromotionDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public decimal Value { get; set; } 
        public string ImageUrl { get; set; }
        public bool HasPercent { get; set; } 
    }
}
