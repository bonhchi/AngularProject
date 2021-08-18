using Common.Constants;
using Common.Pagination;
using Domain.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Orders;
using Service.Files;
using System;
using System.Threading.Tasks;

namespace BE.Controllers.FEAdmins
{
    [Route(UrlConstants.BaseOrder)]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IAuthService authService, IUserManager userManager, IFileService fileService) : base(authService, userManager, fileService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchPaginationDTO<OrderDTO> serachPagination)
        {
            var result = await _orderService.SearchPaginationAsync(serachPagination);
            return CommonResponse(result);
        }
        [HttpGet]
        [Route(UrlConstants.OrderStatus)]
        public async Task<IActionResult> GetByStatus([FromQuery] string status)
        {
            var result = await _orderService.GetByStatus(status);
            return CommonResponse(result);
        }

        [HttpGet]
        [Route(UrlConstants.OrderId)]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _orderService.GetById(id);
            return CommonResponse(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO model)
        {
            var result = await _orderService.CreateAsync(model);
            return CommonResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDTO model)
        {
            var result = await _orderService.UpdateAsync(model);
            return CommonResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteOrderDTO model)
        {
            var result = await _orderService.DeleteAsync(model);
            return CommonResponse(result);
        }
    }
}
