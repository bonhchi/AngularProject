using Common.Constants;
using Common.Pagination;
using Domain.DTOs.PromotionDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.PromotionDetails;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get([FromQuery] SearchPaginationDTO<PromotionDetailDTO> serachPagination)
        {

            var result = _promotionDetailsService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePromotionDetailDTO model)
        {
            var result = _promotionDetailsService.Create(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdatePromotionDetailDTO model)
        {
            var result = _promotionDetailsService.Update(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DeletePromotionDetailDTO model)
        {
            var result = _promotionDetailsService.Delete(model);
            return CommonResponse(result);
        }
    }
}
