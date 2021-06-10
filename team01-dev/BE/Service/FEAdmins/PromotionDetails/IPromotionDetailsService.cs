using Common.Http;
using Common.Pagination;
using Domain.DTOs.PromotionDetails;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PromotionDetails
{
    public interface IPromotionDetailsService : ICommonCRUDService<PromotionDetailDTO, CreatePromotionDetailDTO, UpdatePromotionDetailDTO, DeletePromotionDetailDTO>
    {
        ReturnMessage<PaginatedList<PromotionDetailDTO>> SearchPagination(SearchPaginationDTO<PromotionDetailDTO> search);
    }
}
