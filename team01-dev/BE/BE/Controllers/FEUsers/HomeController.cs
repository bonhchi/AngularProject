using BE.Controllers;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Home;
using System.Collections.Generic;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseHome)]
    [ApiController]
    public class HomeController : BaseController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _homeService = homeService;
        }

        [HttpGet(UrlConstants.TopCollection)]
        public IActionResult GetTopCollectionProducts()
        {

            var result = _homeService.GetTopCollectionProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.NewProducts)]
        public IActionResult GetNewProducts()
        {

            var result = _homeService.GetNewProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.BestSeller)]
        public IActionResult GetBestSellerProducts()
        {

            var result = _homeService.GetBestSellerProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.FeaturedProducts)]
        public IActionResult GetFeaturedProducts()
        {

            var result = _homeService.GetFeaturedProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.Blogs)]
        public IActionResult GetBlogs()
        {

            var result = _homeService.GetBlogs();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.Banners)]
        public IActionResult GetBanners()
        {

            var result = _homeService.GetBanners();
            return CommonResponse(result);
        }
    }
}
