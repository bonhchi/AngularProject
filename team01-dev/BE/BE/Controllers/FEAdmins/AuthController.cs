using Common.Constants;
using Common.Http;
using Common.MD5;
using Domain.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Users;
using System;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers.FEAdmins
{
    [Route(UrlConstants.BaseAuth)]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService,
            IUserManager authManager,
            IFileService fileService) : base(authService, authManager, fileService)
        {
            _authService = authService;
        }

        [HttpPost(UrlConstants.BaseLogin)]
        public IActionResult Login([FromBody] UserLoginDTO data)
        {
            var result = _authService.CheckLogin(data);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetInformation()
        {
            var userDataReturn = _authService.GetInformationUser();
            return CommonResponse(userDataReturn);

        }
    }
}
