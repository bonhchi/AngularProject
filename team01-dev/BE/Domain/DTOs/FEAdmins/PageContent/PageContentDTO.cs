using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.PageContent
{
    public class PageContentDTO: BaseDTO
    {
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
    }
}
