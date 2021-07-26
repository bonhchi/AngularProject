using Common.Http;
using Common.Pagination;
using Domain.DTOs.Products;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Products
{
    public interface IProductService : ICommonCRUDService<SubcategoryDTO, CreateProductDTO, UpdateProductDTO, DeleteProductDTO>
    {
        Task<ReturnMessage<PaginatedList<SubcategoryDTO>>> SearchPaginationAsync(SearchPaginationDTO<SubcategoryDTO> search);
        Task<ReturnMessage<List<SubcategoryDTO>>> GetByCategory(Guid id);
        Task<ReturnMessage<SubcategoryDTO>> GetById(Guid id);
        Task<ReturnMessage<UpdateProductDTO>> UpdateCount(UpdateProductDTO product ,int quantity);

    }
}
