using Common.Http;
using Common.Pagination;
using Domain.DTOs.Blogs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.UserBlogs
{
    public interface IUserBlogService 
    {
        Task<ReturnMessage<BlogDTO>> GetBlog(Guid id);
        Task<ReturnMessage<List<BlogDTO>>> TopBlog(List<BlogDTO> model);
        Task<ReturnMessage<List<BlogDTO>>> RecentBlog(List<BlogDTO> model);
        Task<ReturnMessage<PaginatedList<BlogDTO>>> SearchPaginationAsync(SearchPaginationDTO<BlogDTO> search);
    }
}
