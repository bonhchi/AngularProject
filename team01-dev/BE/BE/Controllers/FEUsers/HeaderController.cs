using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Header;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetHeader()
        {
            var result = await _headerService.GetHeader();
            return CommonResponse(result);
        }
    }
}
