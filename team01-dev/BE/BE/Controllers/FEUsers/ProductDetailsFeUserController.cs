using BE.Controllers;
using Common.Constants;
using Domain.DTOs.ProductsFeUser;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.ProductDetailsFeUser;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseProductDetailsFeUser)]
    [ApiController]
    public class ProductDetailsFeUserController : BaseController
    {
        private readonly IProductDetailsFeService _productDetailsFeService;

        public ProductDetailsFeUserController(IProductDetailsFeService productDetailsFeService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _productDetailsFeService = productDetailsFeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductDTOFeUser search)
        {
            var result = await _productDetailsFeService.GetDetails(search);
            return CommonResponse(result);
        }
    }

}
