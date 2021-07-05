﻿using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Coupons;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route(UrlConstants.BaseCoupon)]
    [ApiController]
    public class UserCouponController : BaseController
    {
        private readonly ICouponService _couponService;

        public UserCouponController(ICouponService couponService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        [Route(UrlConstants.CouponCode)]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {
            var result = await _couponService.GetByCode(code);
            return CommonResponse(result);
        }
    }
}
