using Common.Http;
using Common.Pagination;
using Domain.DTOs.Promotions;
using Infrastructure.EntityFramework;
using Service.Common;

namespace Service.Promotions
{
    public interface IPromotionService : ICommonCRUDService<PromotionDTO, CreatePromotionDTO, UpdatePromotionDTO, DeletePromotionDTO>
    {
        ReturnMessage<PaginatedList<PromotionDTO>> SearchPagination(SearchPaginationDTO<PromotionDTO> search);
    }
}
