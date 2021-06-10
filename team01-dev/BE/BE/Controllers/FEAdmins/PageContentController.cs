using BE.Controllers;
using Common.Constants;
using Domain.DTOs.PageContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Home;
using Service.PageContents;
using System;

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
        public IActionResult Get()
        {
            var result = _pageContentService.GetList();
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreatePageContentDTO model)
        {
            var result = _pageContentService.Create(model);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] UpdatePageContentDTO model)
        {
            var result = _pageContentService.Update(model);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromQuery] DeletePageContentDTO model)
        {
            var result = _pageContentService.Delete(model);
            return CommonResponse(result);
        }
    }
}
