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
        public async Task<IActionResult> Get()
        {
            var result = await _infomationWebService.GetInfo();
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateInformationWebDTO model)
        {
            var result =  await _infomationWebService.UpdateAsync(model);
            return CommonResponse(result);
        }
    }
}
