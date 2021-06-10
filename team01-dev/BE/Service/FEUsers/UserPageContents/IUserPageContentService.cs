using Common.Http;
using Domain.DTOs.PageContent;
using Domain.DTOs.PageContentContact;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.UserPageContents
{
    public interface IUserPageContentService
    {
        public ReturnMessage<List<PageContentDTO>> GetList();
        public ReturnMessage<PageContentDTO> GetById(Guid id);
    }
}
