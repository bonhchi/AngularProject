using Common.Constants;
using Domain.DTOs.InfomationWeb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.InformationWebsiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseInformationWebsite)]
    [ApiController]
    public class InformationWebsiteController : BaseController
    {
        private readonly IInfomationWebService _infomationWebService;
        public InformationWebsiteController(IInfomationWebService infomationWebService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _infomationWebService = infomationWebService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var result = _infomationWebService.GetInfo();
            return CommonResponse(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateInformationWebDTO model)
        {
            var result = _infomationWebService.Update(model);
            return CommonResponse(result);
        }
    }
}
