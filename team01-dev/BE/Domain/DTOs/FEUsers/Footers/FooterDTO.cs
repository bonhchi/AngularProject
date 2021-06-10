using Domain.DTOs.Categories;
using Domain.DTOs.InfomationWeb;
using Domain.DTOs.PageContent;
using Domain.DTOs.SocialMedias;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.FEUsers.Footers
{
    public class FooterDTO
    {
        public List<CategoryDTO> categories { get; set; }
        public List<SocialMediaDTO> socialMedias { get; set; }
        public List<PageContentDTO> pageContents { get; set; }
        public InformationWebDTO informationWeb { get; set; }
    }
}
