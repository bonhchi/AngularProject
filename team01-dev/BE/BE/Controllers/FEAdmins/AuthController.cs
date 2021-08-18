using Common.Constants;
using Domain.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login([FromBody] UserLoginDTO data)
        {
            var result = await _authService.CheckLogin(data);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetInformation()
        {
            var userDataReturn = await _authService.GetInformationUser();
            return CommonResponse(userDataReturn);
        }
    }
}
