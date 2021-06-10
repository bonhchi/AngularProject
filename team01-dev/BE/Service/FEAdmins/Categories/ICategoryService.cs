using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Infrastructure.EntityFramework;
using Service.Common;
namespace Service.Categories
{
    public interface ICategoryService : ICommonCRUDService<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, DeleteCategoryDTO>
    {
        ReturnMessage<PaginatedList<CategoryDTO>> SearchPagination(SearchPaginationDTO<CategoryDTO> search);
    }
}
