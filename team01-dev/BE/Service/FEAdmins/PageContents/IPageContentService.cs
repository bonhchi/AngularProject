using Common.Http;
using Domain.DTOs.PageContent;
using Domain.DTOs.PageContentContact;
using Domain.Entities;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PageContents
{
    public interface IPageContentService: ICommonCRUDService<PageContentDTO, CreatePageContentDTO, UpdatePageContentDTO, DeletePageContentDTO>
    {
        public ReturnMessage<List<PageContentDTO>> GetList();
        public ReturnMessage<PageContentDTO> GetById(Guid id);
    }
}
