using Domain.DTOs.Categories;
using Domain.DTOs.InfomationWeb;
using Domain.DTOs.PageContent;
using Domain.DTOs.SocialMedias;
using System.Collections.Generic;

namespace Domain.DTOs.FEUsers.Footers
{
    public class FooterDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<SocialMediaDTO> SocialMedias { get; set; }
        public List<PageContentDTO> PageContents { get; set; }
        public InformationWebDTO InformationWeb { get; set; }
    }
}
