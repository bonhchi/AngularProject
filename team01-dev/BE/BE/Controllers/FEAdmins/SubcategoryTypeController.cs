using Common.Constants;
using Domain.DTOs.FEAdmins.SubcategoryType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.FEAdmins.SubcategoryTypes;
using Service.Files;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseSubcategoryType)]
    [ApiController]
    public class SubcategoryTypeController : BaseController
    {
        private readonly ISubcategoryTypeService _subcategoryTypeService;

        public SubcategoryTypeController(ISubcategoryTypeService subcategoryTypeService,IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _subcategoryTypeService = subcategoryTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List<SubcategoryTypeDTO> model)
        {
            var result = await _subcategoryTypeService.GetSubcategoryType(model);
            return CommonResponse(result);
        }
    }
}
