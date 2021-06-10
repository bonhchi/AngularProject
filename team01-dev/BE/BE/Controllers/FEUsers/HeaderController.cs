using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Header;


namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseHeader)]
    [ApiController]

    public class HeaderController : BaseController
    {
        private readonly IHeaderService _headerService;

        public HeaderController(IHeaderService headerService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _headerService = headerService;
        }

        //[HttpGet]
        //[Route("categories")]
        //public IActionResult GetCategories()
        //{
        //    var result = _headerService.GetCategories();
        //    return CommonResponse(result);
        //}

        //[HttpGet]
        //[Route("social-medias")]
        //public IActionResult GetSocialMedias()
        //{
        //    var result = _headerService.GetSocialMedias();
        //    return CommonResponse(result);
        //}

        //[HttpGet]
        //[Route("blogs")]
        //public IActionResult GetBlogs()
        //{
        //    var result = _headerService.GetBlogs();
        //    return CommonResponse(result);
        //}

        [HttpGet]
        public IActionResult GetHeader()
        {
            var result = _headerService.GetHeader();
            return CommonResponse(result);
        }
    }
}
