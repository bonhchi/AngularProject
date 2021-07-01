using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Domain.DTOs.Products;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserProductList
{
    public interface IUserProductListService
    {
        public ReturnMessage<IEnumerable<CategoryDTO>> GetCategory();
        ReturnMessage<List<ProductDTO>> GetByCategory(Guid id);
        Task<ReturnMessage<PaginatedList<ProductDTO>>> SearchPagination(SearchPaginationUserFEDTO<ProductDTO> search);
        ReturnMessage<List<ProductDTO>> RelevantProduct(string name);
    }
}
