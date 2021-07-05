using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Customers;
using Service.Files;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Authorize]
    [Route(UrlConstants.BaseCustomer)]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(IAuthService authService, IUserManager userManager, IFileService fileService, ICustomerService customerService) : base(authService, userManager, fileService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<CustomerDTO> serachPagination)
        {
            var result = await _customerService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO model)
        {
            var result = await _customerService.CreateAsync(model);
            if (result.HasError)
            {
                return CommonResponse(result);
            }
            var uploadImage = await _fileService.UpdateIdFile(model.Files, result.Data.Id);
            if (uploadImage.HasError)
            {
                return CommonResponse(uploadImage);
            }
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerDTO model)
        {
            var result = await _customerService.UpdateAsync(model);
            if (result.HasError)
            {
                return CommonResponse(result);
            }
            var uploadImage = await _fileService.UpdateIdFile(model.Files, result.Data.Id);
            if (uploadImage.HasError)
            {
                return CommonResponse(uploadImage);
            }
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCustomerDTO model)
        {
            var result = await _customerService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
