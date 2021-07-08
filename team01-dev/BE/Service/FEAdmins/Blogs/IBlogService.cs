using Common.Http;
using Common.Pagination;
using Domain.DTOs.Blogs;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.Blogs
{
    public interface IBlogService : ICommonCRUDService<BlogDTO, CreateBlogDTO, UpdateBlogDTO, DeleteBlogDTO>
    {
        Task<ReturnMessage<PaginatedList<BlogDTO>>> SearchPaginationAsync(SearchPaginationDTO<BlogDTO> search);
    }
}
