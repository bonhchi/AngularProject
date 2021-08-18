using Common.Http;
using Common.Pagination;
using Domain.DTOs.Coupons;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.Coupons
{
    public interface ICouponService: ICommonCRUDService<CouponDTO, CreateCouponDTO, UpdateCouponDTO, DeleteCouponDTO>
    {
        Task<ReturnMessage<PaginatedList<CouponDTO>>> SearchPaginationAsync(SearchPaginationDTO<CouponDTO> search);
        Task<ReturnMessage<CouponDTO>> GetByCode(string code);
    }
}
