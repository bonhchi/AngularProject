using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Home;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetTopCollectionProducts()
        {
            var result = await _homeService.GetTopCollectionProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.NewProducts)]
        public async Task<IActionResult> GetNewProducts()
        {
            var result = await _homeService.GetNewProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.BestSeller)]
        public async Task<IActionResult> GetBestSellerProducts()
        {
            var result = await _homeService.GetBestSellerProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.FeaturedProducts)]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var result = await _homeService.GetFeaturedProducts();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.Blogs)]
        public async Task<IActionResult> GetBlogs()
        {
            var result = await _homeService.GetBlogs();
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.Banners)]
        public async Task<IActionResult> GetBanners()
        {
            var result = await _homeService.GetBanners();
            return CommonResponse(result);
        }
    }
}
