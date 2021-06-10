using BE.Controllers;
using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.UserProductList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseProductList)]
    [ApiController]
    public class UserProductListController : BaseController
    {
        private readonly IUserProductListService _userProductListService;

        public UserProductListController(IUserProductListService userProductListService, IAuthService authService,
            IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _userProductListService = userProductListService;
        }

        [HttpGet]
        [Route(UrlConstants.Category)]
        public IActionResult GetCategory()
        {
            var result = _userProductListService.GetCategory();
            return CommonResponse(result);
        }

        [HttpGet]
        [Route(UrlConstants.Product)]
        public IActionResult GetProduct([FromQuery] SearchPaginationUserFEDTO<ProductDTO> dto)
        {
            var result = _userProductListService.SearchPagination(dto);
            return CommonResponse(result);
        }
        [HttpGet]
        [Route(UrlConstants.ByCategory)]
        public IActionResult GetByCategory([FromQuery] Guid id)
        {
            var result = _userProductListService.GetByCategory(id);
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.RevelantProduct)]
        public IActionResult RelevantProduct([FromQuery] string name)
        {
            var result = _userProductListService.RelevantProduct(name);
            return CommonResponse(result);
        }
    }
}
