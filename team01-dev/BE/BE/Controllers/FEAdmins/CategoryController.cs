using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Categories;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseCategory)]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<CategoryDTO> serachPagination)
        {
            var result = await _categoryService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO model)
        {
            var result = await _categoryService.CreateAsync(model);
            if (result.HasError)
            {
                return CommonResponse(result);
            }
            var uploadImage = await _fileService.UpdateIdFile(model.Files, result.Data.Id);
            if (uploadImage.HasError)
            {
                return CommonResponse(uploadImage);
            }
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO model)
        {
            var result = await _categoryService.UpdateAsync(model);
            if (result.HasError)
            {
                return CommonResponse(result);
            }
            var uploadImage = await _fileService.UpdateIdFile(model.Files, result.Data.Id);
            if (uploadImage.HasError)
            {
                return CommonResponse(uploadImage);
            }
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCategoryDTO model)
        {
            var result = await _categoryService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
