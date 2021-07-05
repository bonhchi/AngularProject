using Common.Http;
using Common.Pagination;
using Domain.DTOs.OrderDetails;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OrderDetails

{
    public interface IOrderDetailService : ICommonCRUDService<OrderDetailDTO, CreateOrderDetailDTO, UpdateOrderDetailDTO, DeleteOrderDetailDTO>
    {
        Task<ReturnMessage<PaginatedList<OrderDetailDTO>>> SearchPaginationAsync(SearchPaginationDTO<OrderDetailDTO> search);
        Task<ReturnMessage<List<OrderDetailDTO>>> GetByOrder(Guid Id);

    }
}
