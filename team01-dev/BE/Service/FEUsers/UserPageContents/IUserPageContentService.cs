using Common.Http;
using Domain.DTOs.PageContent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.UserPageContents
{
    public interface IUserPageContentService
    {
        public Task<ReturnMessage<List<PageContentDTO>>> GetList();
        public Task<ReturnMessage<PageContentDTO>> GetById(Guid id);
    }
}
