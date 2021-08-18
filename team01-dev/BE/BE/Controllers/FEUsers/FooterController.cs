using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Footer;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseFooter)]
    [ApiController]
    public class FooterController : BaseController
    {
        private readonly IFooterService _footerService;

        public FooterController(IFooterService footerService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _footerService = footerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFooter()
        {
            var result = await _footerService.GetFooter();
            return CommonResponse(result);
        }
    }
}
