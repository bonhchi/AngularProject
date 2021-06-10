using Common.Http;
using Common.Pagination;
using Domain.DTOs.Customer;
using Domain.DTOs.Users;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Customers
{
    public interface ICustomerService : ICommonCRUDService<CustomerDTO, CreateCustomerDTO, UpdateCustomerDTO, DeleteCustomerDTO>
    {
        ReturnMessage<PaginatedList<CustomerDTO>> SearchPagination(SearchPaginationDTO<CustomerDTO> search);
    }
}
