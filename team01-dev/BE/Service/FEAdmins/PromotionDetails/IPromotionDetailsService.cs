using Common.Http;
using Common.Pagination;
using Domain.DTOs.PromotionDetails;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.PromotionDetails
{
    public interface IPromotionDetailsService : ICommonCRUDService<PromotionDetailDTO, CreatePromotionDetailDTO, UpdatePromotionDetailDTO, DeletePromotionDetailDTO>
    {
        Task<ReturnMessage<PaginatedList<PromotionDetailDTO>>> SearchPaginationAsync(SearchPaginationDTO<PromotionDetailDTO> search);
    }
}
