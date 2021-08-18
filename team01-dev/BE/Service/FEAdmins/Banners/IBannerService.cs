using Common.Http;
using Common.Pagination;
using Domain.DTOs.Banners;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.Banners
{
    public interface IBannerService : ICommonCRUDService<BannerDTO, CreateBannerDTO, UpdateBannerDTO, DeleteBannerDTO>
    {
        Task<ReturnMessage<PaginatedList<BannerDTO>>> SearchPaginationAsync(SearchPaginationDTO<BannerDTO> search);
    }
}
