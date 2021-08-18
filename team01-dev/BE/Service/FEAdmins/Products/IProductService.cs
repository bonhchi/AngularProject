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
    public interface IProductService : ICommonCRUDService<ProductDTO, CreateProductDTO, UpdateProductDTO, DeleteProductDTO>
    {
        Task<ReturnMessage<PaginatedList<ProductDTO>>> SearchPaginationAsync(SearchPaginationDTO<ProductDTO> search);
        Task<ReturnMessage<List<ProductDTO>>> GetByCategory(Guid id);
        Task<ReturnMessage<ProductDTO>> GetById(Guid id);
        Task<ReturnMessage<UpdateProductDTO>> UpdateCount(UpdateProductDTO product ,int quantity);
    }
}
