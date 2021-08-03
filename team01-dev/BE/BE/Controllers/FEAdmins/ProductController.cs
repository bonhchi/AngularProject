using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Products;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseProduct)]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<ProductDTO> serachPagination)
        {
            var result = await _productService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO model)
        {
            var result = await _productService.CreateAsync(model);
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
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO model)
        {
            var result = await _productService.UpdateAsync(model);
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
        public async Task<IActionResult> Delete([FromQuery] DeleteProductDTO model)
        {
            var result = await _productService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
