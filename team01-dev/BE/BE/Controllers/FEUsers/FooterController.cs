using BE.Controllers;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.Footer;
using Service.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseFooter)]
    [ApiController]

    public class FooterController : BaseController
    {
        private readonly IFooterService _footerService;

        public FooterController(IFooterService footerService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _footerService = footerService;
        }

        [HttpGet]
        public IActionResult GetFooter()
        {
            var result = _footerService.GetFooter();
            return CommonResponse(result);
        }
    }
}
