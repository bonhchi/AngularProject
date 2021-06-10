using Common.Constants;
using Domain.DTOs.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Contacts;
using Service.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseUserContact)]
    [ApiController]
    public class UserContactController : BaseController
    {
        private readonly IContactService _contactService;

        public UserContactController(IContactService contactService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateContactDTO model)
        {

            var result = _contactService.Create(model);
            return CommonResponse(result);
        }
    }
}
