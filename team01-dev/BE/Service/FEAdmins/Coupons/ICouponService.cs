using Common.Http;
using Common.Pagination;
using Domain.DTOs.Coupons;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Coupons
{
    public interface ICouponService: ICommonCRUDService<CouponDTO, CreateCouponDTO, UpdateCouponDTO, DeleteCouponDTO>
    {
        ReturnMessage<PaginatedList<CouponDTO>> SearchPagination(SearchPaginationDTO<CouponDTO> search);
        ReturnMessage<CouponDTO> GetByCode(string code);

    }
}
