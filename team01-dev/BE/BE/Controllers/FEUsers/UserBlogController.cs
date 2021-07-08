using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Blogs;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.UserBlogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [ApiController]
    public class UserBlogController : BaseController
    {
        private readonly IUserBlogService _userBlogService;

        public UserBlogController(IUserBlogService userBlogService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _userBlogService = userBlogService;
        }

        [HttpGet(UrlConstants.GetUserBlog)]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<BlogDTO> serachPagination)
        {
            var result = await _userBlogService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.TopBlog)]
        public async Task<IActionResult> TopBlog([FromQuery] List<BlogDTO> model)
        {
            var result = await _userBlogService.TopBlog(model);
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.RecentBlog)]
        public async Task<IActionResult> RecentBlog([FromQuery] List<BlogDTO> model)
        {
            var result = await _userBlogService.RecentBlog(model);
            return CommonResponse(result);        }

        [HttpGet(UrlConstants.GetBlog)]
        public async Task<IActionResult> GetBlog(Guid id)
        {
            var result = await _userBlogService.GetBlog(id);
            return CommonResponse(result);
        }

        
    }
}
