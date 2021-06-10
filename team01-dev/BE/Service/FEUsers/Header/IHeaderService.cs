using Common.Http;
using Domain.DTOs.Blogs;
using Domain.DTOs.Categories;
using Domain.DTOs.FEUsers.Headers;
using Domain.DTOs.SocialMedias;
using System.Collections.Generic;

namespace Service.Header
{
    public interface IHeaderService 
    {
        //ReturnMessage<List<SocialMediaDTO>> GetSocialMedias();
        ReturnMessage<HeaderDTO> GetHeader();

    }
}
