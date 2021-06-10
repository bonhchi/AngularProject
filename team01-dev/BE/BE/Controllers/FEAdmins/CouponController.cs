using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Coupons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Coupons;
using Service.Files;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get([FromQuery] SearchPaginationDTO<CouponDTO> serachPagination)
        {
            var result = _couponService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCouponDTO model)
        {
            var result = _couponService.Create(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateCouponDTO model)
        {
            var result = _couponService.Update(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteCouponDTO model)
        {
            var result = _couponService.Delete(model);
            return CommonResponse(result);
        }

    }
}
