using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Blogs
{
    public class BlogDTO : BaseDTO
    {
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string ContentHTML { get; set; }
        public string ImageUrl { get; set; }
        public decimal RatingScore { get; set; }

    }
}
