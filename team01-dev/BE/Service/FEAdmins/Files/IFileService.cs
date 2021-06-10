using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Domain.DTOs.Files;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Files
{
    public interface IFileService : ICommonCRUDService<List<FileDTO>,List<CreateFileDTO>,List<UpdateFileDTO>,List<DeleteFileDTO>>
    {
        ReturnMessage<PaginatedList<FileDTO>> SearchPagination(SearchPaginationDTO<FileDTO> search);
        ReturnMessage<List<FileDTO>> UpdateIdFile(List<FileDTO> files, Guid? entityId);
    }
}
