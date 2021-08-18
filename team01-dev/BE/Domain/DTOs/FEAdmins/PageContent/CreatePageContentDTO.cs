﻿namespace Domain.DTOs.PageContent
{
    public class CreatePageContentDTO
    {
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
    }
}
