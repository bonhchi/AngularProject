using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.OrderDetails;
using Domain.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Files;
using Service.OrderDetails;
using System;

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
        public IActionResult Get([FromQuery] SearchPaginationDTO<OrderDetailDTO> serachPagination)
        {
            var result = _orderDetailService.SearchPagination(serachPagination);
            return CommonResponse(result);
        }

        [HttpGet]
        [Route("order")]
        public IActionResult GetByOrder([FromQuery]Guid id)
        {
            var result = _orderDetailService.GetByOrder(id);
            return CommonResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateOrderDetailDTO model)
        {
            var result = _orderDetailService.Create(model);
            //if (model.Files.IsNullOrEmpty() || result.HasError)
            //{
            //    return CommonResponse(result);
            //}
            //var uploadImage = _fileService.UpdateIdFile(model.Files, result.Data.Id);
            //if (uploadImage.HasError)
            //{
            //    return CommonResponse(uploadImage);
            //}
            return CommonResponse(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateOrderDetailDTO model)
        {
            var result = _orderDetailService.Update(model);
            //if (model.Files.IsNullOrEmpty() || result.HasError)
            //{
            //    return CommonResponse(result);
            //}
            //var uploadImage = _fileService.UpdateIdFile(model.Files, result.Data.Id);
            //if (uploadImage.HasError)
            //{
            //    return CommonResponse(uploadImage);
            //}
            return CommonResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteOrderDetailDTO model)
        {
            var result = _orderDetailService.Delete(model);
            return CommonResponse(result);
        }
    }
}
