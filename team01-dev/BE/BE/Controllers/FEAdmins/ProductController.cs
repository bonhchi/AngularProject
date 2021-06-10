using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Products;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Products;


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
        public IActionResult Get([FromQuery] SearchPaginationDTO<ProductDTO> serachPagination)
        {

            var result = _productService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDTO model)
        {
            var result = _productService.Create(model);
            if (result.HasError)
            {
                return CommonResponse(result);
            }
            var uploadImage = _fileService.UpdateIdFile(model.Files, result.Data.Id);
            if (uploadImage.HasError)
            {
                return CommonResponse(uploadImage);
            }
            return CommonResponse(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateProductDTO model)
        {
            var result = _productService.Update(model);
            if (result.HasError)
            {
                return CommonResponse(result);
            }
            var uploadImage = _fileService.UpdateIdFile(model.Files, result.Data.Id);
            if (uploadImage.HasError)
            {
                return CommonResponse(uploadImage);
            }
            return CommonResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteProductDTO model)
        {
            var result = _productService.Delete(model);
            return CommonResponse(result);
        }
    }
}
