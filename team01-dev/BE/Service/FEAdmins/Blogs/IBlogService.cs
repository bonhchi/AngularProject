using Common.Http;
using Common.Pagination;
using Domain.DTOs.Blogs;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Blogs
{
    public interface IBlogService : ICommonCRUDService<BlogDTO, CreateBlogDTO, UpdateBlogDTO, DeleteBlogDTO>
    {
        ReturnMessage<PaginatedList<BlogDTO>> SearchPagination(SearchPaginationDTO<BlogDTO> search);

      
    }
}
