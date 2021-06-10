using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Promotions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BasePromotion)]
    [ApiController]
    public class PromotionController : BaseController
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] SearchPaginationDTO<PromotionDTO> serachPagination)
        {

            var result = _promotionService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePromotionDTO model)
        {
            var result = _promotionService.Create(model);
            if (model.Files.IsNullOrEmpty() || result.HasError)
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
        public IActionResult Update([FromBody] UpdatePromotionDTO model)
        {
            var result = _promotionService.Update(model);
            if (model.Files.IsNullOrEmpty() || result.HasError)
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
        public IActionResult Delete([FromQuery] DeletePromotionDTO model)
        {
            var result = _promotionService.Delete(model);
            return CommonResponse(result);
        }
    }
}
