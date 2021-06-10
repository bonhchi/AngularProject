using BE.Controllers;
using Common.Constants;
using Domain.DTOs.Customer;
using Domain.DTOs.CustomerFE;
using Infrastructure.Mails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.AuthCustomer;
using Service.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseAuthCustomer)]
    [ApiController]
    public class AuthCustomerController : BaseController
    {
        private readonly IAuthCustomerUserService _authCustomerService;
        private readonly IEmailService _emailService;
        public AuthCustomerController(IAuthService authService, IUserManager userManager, IFileService fileService, IAuthCustomerUserService authCustomerService, IEmailService emailService) : base(authService, userManager, fileService)
        {
            _authCustomerService = authCustomerService;
            _emailService = emailService;
        }

        [HttpPost(UrlConstants.BaseLoginCustomer)]
        public IActionResult Login([FromBody] CustomerLoginDTO data)
        {
            var result = _authCustomerService.CheckLogin(data);
            return CommonResponse(result);
        }

        [HttpPost(UrlConstants.BaseRegistCustomer)]
        public IActionResult Register([FromBody] CustomerRegisterDTO data)
        {
            var result = _authCustomerService.CheckRegister(data);
            return CommonResponse(result);
        }

        [HttpGet("forgetpassword")]
        public IActionResult Forgetpassword ([FromQuery] CustomerEmailDTO model)
        {
            var result = _authCustomerService.ForgetPassword(model);
            return CommonResponse(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetInfo()
        {
            var result = _authCustomerService.GetCustomerDataReturnDTO();
            return CommonResponse(result);
        }
    }
}
