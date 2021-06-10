using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.SocialMedias;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.SocialMedias
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IRepository<SocialMedia> _socialMediaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SocialMediaService(IRepository<SocialMedia> socialmediaRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _socialMediaRepository = socialmediaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ReturnMessage<SocialMediaDTO> Create(CreateSocialMediaDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            if(model.Title == null)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var entity = _mapper.Map<CreateSocialMediaDTO, SocialMedia>(model);
                entity.Insert();
                _socialMediaRepository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<SocialMediaDTO>(false, _mapper.Map<SocialMedia, SocialMediaDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<SocialMediaDTO> Delete(DeleteSocialMediaDTO model)
        {
            try
            {
                var entity = _socialMediaRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _socialMediaRepository.Delete(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<SocialMediaDTO>(false, _mapper.Map<SocialMedia, SocialMediaDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<SocialMediaDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<SocialMediaDTO> Update(UpdateSocialMediaDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            if (model.Title == null)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var entity = _socialMediaRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    _socialMediaRepository.Update(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<SocialMediaDTO>(false, _mapper.Map<SocialMedia, SocialMediaDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<SocialMediaDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PaginatedList<SocialMediaDTO>> SearchPagination(SearchPaginationDTO<SocialMediaDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<SocialMediaDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var resultEntity = _socialMediaRepository.GetPaginatedList(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.Title.Contains(search.Search.Title) ||
                        it.Link.Contains(search.Search.Link) ||
                        it.IconUrl.Contains(search.Search.IconUrl)
                    )
                ) && !it.IsDeleted
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => -t.DisplayOrder
            );
            var data = _mapper.Map<PaginatedList<SocialMedia>, PaginatedList<SocialMediaDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<SocialMediaDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }
    }
}
