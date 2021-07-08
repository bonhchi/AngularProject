using Common.Constants;
using Domain.DTOs.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Profiles;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseProfile)]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _profileService = profileService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProfileDTO model)
        {
            var result = await _profileService.UpdateAsync(model);
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
        [Route(UrlConstants.Password)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassworProfileDTO model)
        {
            var result = await _profileService.ChangePassword(model);
            return CommonResponse(result);
        }

    }
}
