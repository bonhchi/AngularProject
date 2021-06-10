using BE.Controllers;
using Common.Constants;
using Common.Pagination;
using Domain.DTOs.CustomerWishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Contacts;
using Service.CustomerWishLists;
using Service.Files;
using System;

namespace BE.Controllers.FEUsers
{
    [Route(UrlConstants.BaseCustomerWishList)]
    [ApiController]
    public class CustomerWishListController : BaseController
    {
        private readonly ICustomerWishListService _service;

        public CustomerWishListController(ICustomerWishListService service, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetByCustomer()
        {
            var result = _service.GetByCustomer();
            return CommonResponse(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateOrDelete([FromBody] CreateOrDeleteCustomerWishListDTO model)
        {

            var result = _service.CreateOrDelete(model);
            return CommonResponse(result);
        }
    }
}
