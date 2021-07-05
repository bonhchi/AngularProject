using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Coupons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Coupons;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Route(UrlConstants.BaseCoupon)]
    [ApiController]
    [Authorize]
    public class CouponController : BaseController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<CouponDTO> serachPagination)
        {
            var result = await _couponService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCouponDTO model)
        {
            var result = await _couponService.CreateAsync(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCouponDTO model)
        {
            var result = await _couponService.UpdateAsync(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCouponDTO model)
        {
            var result = await _couponService.DeleteAsync(model);
            return CommonResponse(result);
        }

    }
}
