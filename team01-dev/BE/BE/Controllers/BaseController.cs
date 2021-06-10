using Common.Constants;
using Common.Http;
using Domain.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using System.Collections.Generic;

namespace BE.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IAuthService _authService;
        public readonly IUserManager _userManager;
        public readonly IFileService _fileService;
        public BaseController(IAuthService authService, IUserManager userManager, IFileService fileService)
        {
            _authService = authService;
            _userManager = userManager;
            _fileService = fileService;
        }

        public IActionResult CommonResponse<T>(ReturnMessage<T> data)
        {
            if (data.HasError)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
    }
}
