using Common.Http;
using Common.Pagination;
using Domain.DTOs.Promotions;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.Promotions
{
    public interface IPromotionService : ICommonCRUDService<PromotionDTO, CreatePromotionDTO, UpdatePromotionDTO, DeletePromotionDTO>
    {
        Task<ReturnMessage<PaginatedList<PromotionDTO>>> SearchPaginationAsync(SearchPaginationDTO<PromotionDTO> search);
    }
}
