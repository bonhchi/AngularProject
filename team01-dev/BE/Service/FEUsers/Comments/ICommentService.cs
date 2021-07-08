using Common.Http;
using Common.Pagination;
using Domain.DTOs.Comments;
using Infrastructure.EntityFramework;

using System.Threading.Tasks;

namespace Service.Comments
{
    public interface ICommentService
    {
        Task<ReturnMessage<PaginatedList<CommentDTO>>> BlogPaginationAsync(SearchPaginationDTO<CommentDTO> search);
        Task<ReturnMessage<PaginatedList<CommentDTO>>> ProductPaginationAsync(SearchPaginationDTO<CommentDTO> search);
        Task<ReturnMessage<CommentDTO>> CreateAsync(CreateCommentDTO model);
        Task<ReturnMessage<CommentDTO>> DeleteAsync(DeleteCommentDTO model);
    }
}
