using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.UserProductList;
using System;
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
        public async Task<IActionResult> GetCategory()
        {
            var result = await _userProductListService.GetCategory();
            return CommonResponse(result);
        }

        [HttpGet]
        [Route(UrlConstants.Product)]
        public async Task<IActionResult> GetProduct([FromQuery] SearchPaginationUserFEDTO<SubcategoryDTO> dto)
        {
            var result = await _userProductListService.SearchPaginationAsync(dto);
            return CommonResponse(result);
        }
        [HttpGet]
        [Route(UrlConstants.ByCategory)]
        public async Task<IActionResult> GetByCategory([FromQuery] Guid id)
        {
            var result = await _userProductListService.GetByCategory(id);
            return CommonResponse(result);
        }

        [HttpGet(UrlConstants.RevelantProduct)]
        public async Task<IActionResult> RelevantProduct([FromQuery] string name)
        {
            var result = await _userProductListService.RelevantProduct(name);
            return CommonResponse(result);
        }
    }
}
