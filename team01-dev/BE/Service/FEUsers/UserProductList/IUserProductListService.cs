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
        Task<ReturnMessage<List<ProductDTO>>> GetByCategory(Guid id);
        Task<ReturnMessage<PaginatedList<ProductDTO>>> SearchPaginationAsync(SearchPaginationUserFEDTO<ProductDTO> search);
        Task<ReturnMessage<List<ProductDTO>>> RelevantProduct(string name);
    }
}
