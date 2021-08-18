using Common.Http;
using Common.Pagination;
using Domain.DTOs.Orders;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Orders
{
    public interface IOrderService : ICommonCRUDService<OrderDTO, CreateOrderDTO, UpdateOrderDTO, DeleteOrderDTO>
    {
        Task<ReturnMessage<PaginatedList<OrderDTO>>> SearchPaginationAsync(SearchPaginationDTO<OrderDTO> search);
        Task<ReturnMessage<OrderDTO>> GetById(Guid Id);
        Task<ReturnMessage<List<OrderDTO>>> GetByStatus(string status);
    }
}
