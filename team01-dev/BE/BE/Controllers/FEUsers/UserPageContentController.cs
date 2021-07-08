using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.UserPageContents;
using System;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseUserPageContent)]
    [ApiController]
    public class UserPageContentController : BaseController
    {
        private readonly IUserPageContentService _pageContentService;

        public UserPageContentController(IUserPageContentService pageContentService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _pageContentService = pageContentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _pageContentService.GetList();
            return CommonResponse(result);
        }

        [Route(UrlConstants.GetPageContentUser)]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _pageContentService.GetById(id);
            return CommonResponse(result);
        }
    }
}
