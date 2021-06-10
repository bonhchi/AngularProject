using Common.Constants;
using Common.Http;
using Domain.DTOs.Profiles;
using Domain.DTOs.User;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Profiles;

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
        public IActionResult Update([FromBody] UpdateProfileDTO model)
        {
            var result = _profileService.Update(model);
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
        [Route(UrlConstants.Password)]
        public IActionResult ChangePassword([FromBody] ChangePassworProfileDTO model)
        {
            var result = _profileService.ChangePassword(model);
            return CommonResponse(result);
        }

    }
}
