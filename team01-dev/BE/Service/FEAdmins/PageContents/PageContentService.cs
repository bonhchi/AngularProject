using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.StringEx;
using Domain.Constants;
using Domain.DTOs.PageContent;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.PageContents
{
    public class PageContentService : IPageContentService
    {
        private readonly IRepositoryAsync<PageContent> _pageContentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public PageContentService(IRepositoryAsync<PageContent> pageContentRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _pageContentRepository = pageContentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<ReturnMessage<PageContentDTO>> CreateAsync(CreatePageContentDTO model)
        {
            try
            {
                var entity = _mapper.Map<CreatePageContentDTO, PageContent>(model);
                entity.Insert();
                _pageContentRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var data = _mapper.Map<PageContent, PageContentDTO>(entity);
                return new ReturnMessage<PageContentDTO>(false, data, MessageConstants.CreateSuccess);
            }
            catch
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
            }
        }

        public ReturnMessage<List<PageContentDTO>> GetList()
        {
            try
            {
                var resultEntity = _pageContentRepository.Queryable()
                                    .Where(i => !i.IsDeleted)
                                    .OrderBy(i => i.Order)
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
                var resultEntity = _pageContentRepository.Find(id);
                var data = _mapper.Map<PageContent, PageContentDTO>(resultEntity);
                var result = new ReturnMessage<PageContentDTO>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
            }
        }

        public async Task<ReturnMessage<PageContentDTO>> UpdateAsync(UpdatePageContentDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            model.Description = StringExtension.CleanString(model.Description);
            if (model.Title == null || model.Description == null)
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var entity = await _pageContentRepository.FindAsync(model.Id);
                if (!entity.IsNotNullOrEmpty())
                    return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
                entity.Update(model);
                await _pageContentRepository.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                var data = _mapper.Map<PageContent, PageContentDTO>(entity);
                var result = new ReturnMessage<PageContentDTO>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
            }
        }

        // not use
        public async Task<ReturnMessage<PageContentDTO>> DeleteAsync(DeletePageContentDTO model)
        {
            try
            {
                if (model.Id.IsNullOrEmpty() ||
                    model.Id == PageContentConstants.Shipping ||
                    model.Id == PageContentConstants.AboutUs ||
                    model.Id == PageContentConstants.ContactUs)
                {
                    return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
                }
                var entity = _pageContentRepository.Find(model.Id);
                entity.Delete();
                await _pageContentRepository.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<PageContentDTO>(false, null, MessageConstants.DeleteSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<PageContentDTO>(true, null, MessageConstants.Error);
            }
        }
    }
}
