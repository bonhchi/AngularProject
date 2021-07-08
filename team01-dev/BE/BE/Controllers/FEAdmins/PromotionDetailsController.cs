using Common.Constants;
using Common.Pagination;
using Domain.DTOs.PromotionDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.PromotionDetails;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BasePromotionDetail)]
    [ApiController]
    public class PromotionDetailsController : BaseController
    {
        private readonly IPromotionDetailsService _promotionDetailsService;

        public PromotionDetailsController(IPromotionDetailsService promotionDetailsService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _promotionDetailsService = promotionDetailsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<PromotionDetailDTO> serachPagination)
        {
            var result = await _promotionDetailsService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionDetailDTO model)
        {
            var result = await _promotionDetailsService.CreateAsync(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePromotionDetailDTO model)
        {
            var result = await _promotionDetailsService.UpdateAsync(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeletePromotionDetailDTO model)
        {
            var result = await _promotionDetailsService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
