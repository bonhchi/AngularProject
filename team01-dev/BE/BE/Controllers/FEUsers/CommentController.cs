using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Comments;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseComment)]
    [ApiController]
    public class CommentController: BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _commentService = commentService;
        }

        [Route(UrlConstants.GetCommentBlog)]
        [HttpGet]
        public async Task<IActionResult> GetBlogPagination([FromQuery] SearchPaginationDTO<CommentDTO> serachPagination)
        {
            var result = await _commentService.BlogPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [Route(UrlConstants.GetCommentProduct)]
        [HttpGet]
        public async Task<IActionResult> GetProductPagination([FromQuery] SearchPaginationDTO<CommentDTO> serachPagination)
        {
            var result = await _commentService.ProductPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDTO model)
        {
            var result = await _commentService.CreateAsync(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCommentDTO model)
        {
            var result = await _commentService.DeleteAsync(model);
            return CommonResponse(result);
        }
        

    }
}
