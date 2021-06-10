using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Users;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Users;
using System;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseUser)]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] SearchPaginationDTO<UserDTO> serachPagination)
        {
            var result = _userService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [Authorize]
        [Route(UrlConstants.GetUser)]
        [HttpGet]
        public IActionResult GetDetailUser(Guid id)
        {
            var result = _userService.GetDetailUser(id);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDTO model)
        {
            var result = _userService.Create(model);
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

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateUserDTO model)
        {
            var result = _userService.Update(model);
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

        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteUserDTO model)
        {
            var result = _userService.Delete(model);
            return CommonResponse(result);
        }
    }
}
