using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.Categories
{
    public interface ICategoryService : ICommonCRUDService<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, DeleteCategoryDTO>
    {
        Task<ReturnMessage<PaginatedList<CategoryDTO>>> SearchPaginationAsync(SearchPaginationDTO<CategoryDTO> search);
    }
}
