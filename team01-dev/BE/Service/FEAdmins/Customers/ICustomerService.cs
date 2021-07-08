using Common.Http;
using Common.Pagination;
using Domain.DTOs.Customer;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.Customers
{
    public interface ICustomerService : ICommonCRUDService<CustomerDTO, CreateCustomerDTO, UpdateCustomerDTO, DeleteCustomerDTO>
    {
        Task<ReturnMessage<PaginatedList<CustomerDTO>>> SearchPaginationAsync(SearchPaginationDTO<CustomerDTO> search);
    }
}
