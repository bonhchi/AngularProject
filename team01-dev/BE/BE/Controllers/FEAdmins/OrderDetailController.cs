using Common.Constants;
using Common.Pagination;
using Domain.DTOs.OrderDetails;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.OrderDetails;
using System;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Route(UrlConstants.BaseOrderDetail)]
    [ApiController]
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<OrderDetailDTO> serachPagination)
        {
            var result = await _orderDetailService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }

        [HttpGet]
        [Route(UrlConstants.GetOrder)]
        public async Task<IActionResult> GetByOrder([FromQuery]Guid id)
        {
            var result = await _orderDetailService.GetByOrder(id);
            return CommonResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDetailDTO model)
        {
            var result = await _orderDetailService.CreateAsync(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDetailDTO model)
        {
            var result =  await _orderDetailService.UpdateAsync(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteOrderDetailDTO model)
        {
            var result = await _orderDetailService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
