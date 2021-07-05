using Common.Http;
using Common.Pagination;
using Domain.DTOs.SocialMedias;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.SocialMedias
{
    public interface ISocialMediaService: ICommonCRUDService<SocialMediaDTO, CreateSocialMediaDTO, UpdateSocialMediaDTO, DeleteSocialMediaDTO>
    {
        Task<ReturnMessage<PaginatedList<SocialMediaDTO>>> SearchPaginationAsync(SearchPaginationDTO<SocialMediaDTO> search);
    }
}
