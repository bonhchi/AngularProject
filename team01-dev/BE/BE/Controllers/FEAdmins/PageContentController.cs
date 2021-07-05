using BE.Controllers;
using Common.Constants;
using Domain.DTOs.PageContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.PageContents;
using System.Threading.Tasks;

namespace BE.ControllersFeUser.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BasePageContent)]
    [ApiController]
    public class PageContentController : BaseController
    {
        private readonly IPageContentService _pageContentService;

        public PageContentController(IPageContentService pageContentService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _pageContentService = pageContentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _pageContentService.GetList();
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePageContentDTO model)
        {
            var result = await _pageContentService.CreateAsync(model);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePageContentDTO model)
        {
            var result = await _pageContentService.UpdateAsync(model);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeletePageContentDTO model)
        {
            var result = await _pageContentService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
