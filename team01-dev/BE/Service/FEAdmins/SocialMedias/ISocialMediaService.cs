using Common.Http;
using Common.Pagination;
using Domain.DTOs.SocialMedias;
using Infrastructure.EntityFramework;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.SocialMedias
{
    public interface ISocialMediaService: ICommonCRUDService<SocialMediaDTO, CreateSocialMediaDTO, UpdateSocialMediaDTO, DeleteSocialMediaDTO>
    {
        ReturnMessage<PaginatedList<SocialMediaDTO>> SearchPagination(SearchPaginationDTO<SocialMediaDTO> search);
    }
}
