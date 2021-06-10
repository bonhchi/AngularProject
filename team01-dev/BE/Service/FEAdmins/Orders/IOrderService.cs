using Common.Http;
using Common.Pagination;
using Domain.DTOs.Orders;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;

namespace Service.Orders

{
    public interface IOrderService : ICommonCRUDService<OrderDTO, CreateOrderDTO, UpdateOrderDTO, DeleteOrderDTO>
    {
        ReturnMessage<PaginatedList<OrderDTO>> SearchPagination(SearchPaginationDTO<OrderDTO> search);
        ReturnMessage<OrderDTO> GetById(Guid Id);
        ReturnMessage<List<OrderDTO>> GetByStatus(string status);


    }
}
