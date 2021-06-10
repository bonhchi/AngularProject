using Common.Http;
using Common.Pagination;
using Domain.DTOs.Blogs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;

namespace Service.UserBlogs
{
    public interface IUserBlogService 
    {
        ReturnMessage<BlogDTO> GetBlog(Guid id);
        ReturnMessage<List<BlogDTO>> TopBlog(List<BlogDTO> model);
        ReturnMessage<List<BlogDTO>> RecentBlog(List<BlogDTO> model);
        ReturnMessage<PaginatedList<BlogDTO>> SearchPagination(SearchPaginationDTO<BlogDTO> search);

    }
}
