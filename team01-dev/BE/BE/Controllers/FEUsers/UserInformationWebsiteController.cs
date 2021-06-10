using Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.InformationWebsiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseUserInformationWebsite)]
    [ApiController]
    public class UserInformationWebsiteController : BaseController
    {
        private readonly IInfomationWebService _infomationWebService;
        public UserInformationWebsiteController(IInfomationWebService infomationWebService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _infomationWebService = infomationWebService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var result = _infomationWebService.GetInfo();
            return CommonResponse(result);
        }
    }
}
