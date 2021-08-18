using Common.Constants;
using Domain.DTOs.CustomerFE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.AuthCustomer;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseAuthCustomer)]
    [ApiController]
    public class AuthCustomerController : BaseController
    {
        private readonly IAuthCustomerUserService _authCustomerService;
        public AuthCustomerController(IAuthService authService, IUserManager userManager, IFileService fileService, IAuthCustomerUserService authCustomerService) : base(authService, userManager, fileService)
        {
            _authCustomerService = authCustomerService;
        }

        [HttpPost(UrlConstants.BaseLoginCustomer)]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDTO data)
        {
            var result = await _authCustomerService.CheckLogin(data);
            return CommonResponse(result);
        }

        [HttpPost(UrlConstants.BaseRegistCustomer)]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDTO data)
        {
            var result = await _authCustomerService.CheckRegister(data);
            return CommonResponse(result);
        }

        [HttpGet("forgetpassword")] // const
        public async Task<IActionResult> Forgetpassword ([FromQuery] CustomerEmailDTO model)
        {
            var result = await _authCustomerService.ForgetPassword(model);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var result = await _authCustomerService.GetCustomerDataReturnDTO();
            return CommonResponse(result);
        }
    }
}
