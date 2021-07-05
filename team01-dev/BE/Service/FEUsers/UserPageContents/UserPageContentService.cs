using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.PageContent;
using Domain.Entities;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.UserPageContents
{
    public class UserPageContentService : IUserPageContentService
    {
        private readonly IRepositoryAsync<PageContent> _pageContentRepository;
        private readonly IMapper _mapper;
        public UserPageContentService(IRepositoryAsync<PageContent> pageContentRepository, IMapper mapper)
        {
            _pageContentRepository = pageContentRepository;
            _mapper = mapper;
        }
        public async Task<ReturnMessage<List<PageContentDTO>>> GetList()
        {
            try
            {
                var resultEntity = _pageContentRepository.Queryable()
                                    .OrderBy(i => i.Id).Take(3).Where(i => i.Order >= 0).OrderBy(i => i.Order)
                                    .ToList();
                var data = _mapper.Map<List<PageContent>, List<PageContentDTO>>(resultEntity);
                var result = new ReturnMessage<List<PageContentDTO>>(false, data, MessageConstants.ListSuccess);

                await Task.CompletedTask;
                return result;
            }
            catch
            {
                return new ReturnMessage<List<PageContentDTO>>(true, null, MessageConstants.Error);
            }
        }

        public async Task<ReturnMessage<PageContentDTO>> GetById(Guid id)
        {
            try
            {
                var resultEntity = _pageContentRepository.Queryable().Where(i => i.Id == id && !i.IsDeleted && i.Order >= 0).FirstOrDefault();
                var data = _mapper.Map<PageContent, PageContentDTO>(resultEntity);
                var result = new ReturnMessage<PageContentDTO>(false, data, MessageConstants.ListSuccess);

                await Task.CompletedTask;
                return result;
            }
            catch
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
            }
        }

    }
}
