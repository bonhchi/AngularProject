using Common.Http;
using Common.Pagination;
using Domain.DTOs.Comments;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Comments
{
    public interface ICommentService
    {
        public ReturnMessage<PaginatedList<CommentDTO>> BlogPagination(SearchPaginationDTO<CommentDTO> search);
        public ReturnMessage<PaginatedList<CommentDTO>> ProductPagination(SearchPaginationDTO<CommentDTO> search);
        public Task<ReturnMessage<CommentDTO>> CreateAsync(CreateCommentDTO model);
        public Task<ReturnMessage<CommentDTO>> DeleteAsync(DeleteCommentDTO model);

    }
}
