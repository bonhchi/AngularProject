using Common.Constants;
using Common.Pagination;
using Domain.DTOs.FEAdmins.Subcategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.FEAdmins.Subcategories;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseSubcategory)]
    [ApiController]
    public class SubcategoryController : BaseController
    {
        private readonly ISubcategoryService _subcategoryService;
        public SubcategoryController(ISubcategoryService subcategoryService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<SubcategoryDTO> searchPagination)
        {
            var result = await _subcategoryService.SearchPaginationAsync(searchPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubcategoryDTO model)
        {
            var result = await _subcategoryService.CreateAsync(model);
            return CommonResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSubcategoryDTO model)
        {
            var result = await _subcategoryService.UpdateAsync(model);
            return CommonResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteSubcategoryDTO model)
        {
            var result = await _subcategoryService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
