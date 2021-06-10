using BE.Controllers;
using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Comments;
using Service.Contacts;
using Service.Files;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetBlogPagination([FromQuery] SearchPaginationDTO<CommentDTO> serachPagination)
        {
            var result = _commentService.BlogPagination(serachPagination);
            return CommonResponse(result);
        }

        [Route(UrlConstants.GetCommentProduct)]
        [HttpGet]
        public IActionResult GetProductPagination([FromQuery] SearchPaginationDTO<CommentDTO> serachPagination)
        {
            var result = _commentService.ProductPagination(serachPagination);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentDTO model)
        {
            var result = _commentService.Create(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteCommentDTO model)
        {
            var result = _commentService.Delete(model);
            return CommonResponse(result);
        }
        

    }
}
