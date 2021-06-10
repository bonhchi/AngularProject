using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.StringEx;
using Domain.DTOs.PageContent;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.UserPageContents
{
    public class UserPageContentService : IUserPageContentService
    {
        private readonly IRepository<PageContent> _pageContentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserPageContentService(IRepository<PageContent> pageContentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _pageContentRepository = pageContentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public ReturnMessage<List<PageContentDTO>> GetList()
        {
            try
            {
                var resultEntity = _pageContentRepository.Queryable()
                                    .OrderBy(i => i.Id).Take(3).Where(i => i.Order >= 0).OrderBy(i => i.Order)
                                    .ToList();
                var data = _mapper.Map<List<PageContent>, List<PageContentDTO>>(resultEntity);
                var result = new ReturnMessage<List<PageContentDTO>>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<PageContentDTO>>(true, null, MessageConstants.Error);
            }
        }

        public ReturnMessage<PageContentDTO> GetById(Guid id)

        {
            try
            {
                var resultEntity = _pageContentRepository.Queryable().Where(i => i.Id == id && !i.IsDeleted && i.Order >= 0).FirstOrDefault();
                var data = _mapper.Map<PageContent, PageContentDTO>(resultEntity);
                var result = new ReturnMessage<PageContentDTO>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
            }
        }

    }
}
