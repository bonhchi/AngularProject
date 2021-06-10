using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Home
{
    public class HomeBannerDTO: BaseDTO
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
    }
}
