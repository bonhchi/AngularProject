using Common.Constants;
using Common.Pagination;
using Domain.DTOs.SocialMedias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.SocialMedias;
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
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<SocialMediaDTO> serachPagination)
        {
            var result = await _socialMediaService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocialMediaDTO model)
        {
            var result = await _socialMediaService.CreateAsync(model);
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
        public async Task<IActionResult> Update([FromBody] UpdateSocialMediaDTO model)
        {
            var result = await _socialMediaService.UpdateAsync(model);
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
        public async Task<IActionResult> Delete([FromQuery] DeleteSocialMediaDTO model)
        {
            var result = await _socialMediaService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
