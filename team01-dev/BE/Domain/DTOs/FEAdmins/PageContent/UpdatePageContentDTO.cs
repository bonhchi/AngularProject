using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.PageContent
{
    public class UpdatePageContentDTO : BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
    }
}
