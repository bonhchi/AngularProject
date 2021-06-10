using BE.Controllers;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.SocialMedias;
using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseSocialMedia)]
    [ApiController]
    public class SocialMediaController : BaseController
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] SearchPaginationDTO<SocialMediaDTO> serachPagination)
        {
            var result = _socialMediaService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSocialMediaDTO model)
        {
            var result = _socialMediaService.Create(model);
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
        public IActionResult Update([FromBody] UpdateSocialMediaDTO model)
        {
            var result = _socialMediaService.Update(model);
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
        public IActionResult Delete([FromQuery] DeleteSocialMediaDTO model)
        {
            var result = _socialMediaService.Delete(model);
            return CommonResponse(result);
        }
    }
}
