using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Domain.DTOs.Products;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.UserProductList
{
    public interface IUserProductListService
    {
        Task<ReturnMessage<IEnumerable<CategoryDTO>>> GetCategory();
        Task<ReturnMessage<List<SubcategoryDTO>>> GetByCategory(Guid id);
        Task<ReturnMessage<PaginatedList<SubcategoryDTO>>> SearchPaginationAsync(SearchPaginationUserFEDTO<SubcategoryDTO> search);
        Task<ReturnMessage<List<SubcategoryDTO>>> RelevantProduct(string name);
    }
}
