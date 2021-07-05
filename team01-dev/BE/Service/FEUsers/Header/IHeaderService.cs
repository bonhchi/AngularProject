using Common.Http;
using Domain.DTOs.FEUsers.Headers;
using System.Threading.Tasks;

namespace Service.Header
{
    public interface IHeaderService 
    {
        //ReturnMessage<List<SocialMediaDTO>> GetSocialMedias();
        Task<ReturnMessage<HeaderDTO>> GetHeader();

    }
}
