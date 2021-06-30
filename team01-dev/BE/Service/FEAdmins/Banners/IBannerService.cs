using Common.Http;
using Common.Pagination;
using Domain.DTOs.Banners;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Banners
{
    public interface IBannerService : ICommonCRUDService<BannerDTO, CreateBannerDTO, UpdateBannerDTO, DeleteBannerDTO>
    {
        Task<ReturnMessage<PaginatedList<BannerDTO>>> SearchPagination(SearchPaginationDTO<BannerDTO> search);
    }
}
