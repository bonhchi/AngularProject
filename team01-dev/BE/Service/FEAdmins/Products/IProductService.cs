using Common.Http;
using Common.Pagination;
using Domain.DTOs.Products;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;

namespace Service.Products
{
    public interface IProductService : ICommonCRUDService<ProductDTO, CreateProductDTO, UpdateProductDTO, DeleteProductDTO>
    {
        ReturnMessage<PaginatedList<ProductDTO>> SearchPagination(SearchPaginationDTO<ProductDTO> search);
        ReturnMessage<List<ProductDTO>> GetByCategory(Guid id);
        ReturnMessage<ProductDTO> GetById(Guid id);
        ReturnMessage<UpdateProductDTO> UpdateCount(UpdateProductDTO product ,int quantity);

    }
}
