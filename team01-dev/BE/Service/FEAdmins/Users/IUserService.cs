using Common.Http;
using Common.Pagination;
using Domain.DTOs.Users;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Threading.Tasks;

namespace Service.Users
{
    public interface IUserService : ICommonCRUDService<UserDTO, CreateUserDTO, UpdateUserDTO, DeleteUserDTO>
    {
        ReturnMessage<PaginatedList<UserDTO>> SearchPagination(SearchPaginationDTO<UserDTO> search);
        Task <ReturnMessage<UserDTO>> GetDetailUser(Guid id);
    }
}
