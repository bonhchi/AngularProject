using BE.Controllers;
using Common.Constants;
using Domain.DTOs.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Contacts;
using Service.Files;
using System.Threading.Tasks;

namespace BE.FeUserControllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseContact)]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _contactService.GetList();
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDTO model)
        {
            var result = await _contactService.CreateAsync(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactDTO model)
        {
            var result = await _contactService.UpdateAsync(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteContactDTO model)
        {
            var result = await _contactService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
