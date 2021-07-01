using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Orders;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Orders;
using Service.Files;
using System;
using Service.OrderDetails;
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
        //async
        [HttpGet]
        public IActionResult Get([FromQuery] SearchPaginationDTO<OrderDTO> serachPagination)
        {
            var result = _orderService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }
        [HttpGet]
        [Route(UrlConstants.OrderStatus)]
        //async
        public IActionResult GetByStatus([FromQuery] string status)
        {
            var result = _orderService.GetByStatus(status);
            return CommonResponse(result);
        }

        //async
        [HttpGet]
        [Route(UrlConstants.OrderId)]
        public IActionResult GetById([FromQuery] Guid id)
        {
            var result = _orderService.GetById(id);
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
