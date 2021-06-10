using Common.Http;
using Domain.DTOs.Categories;
using Domain.DTOs.FEUsers.Footers;
using Domain.DTOs.SocialMedias;
using System.Collections.Generic;

namespace Service.Footer
{
    public interface IFooterService
    {
        ReturnMessage<FooterDTO> GetFooter();
    }
}
