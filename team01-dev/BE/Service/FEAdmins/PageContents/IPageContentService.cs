using Common.Http;
using Domain.DTOs.PageContent;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.PageContents
{
    public interface IPageContentService: ICommonCRUDService<PageContentDTO, CreatePageContentDTO, UpdatePageContentDTO, DeletePageContentDTO>
    {
        public Task<ReturnMessage<List<PageContentDTO>>> GetList();
        public Task<ReturnMessage<PageContentDTO>> GetById(Guid id);
    }
}
