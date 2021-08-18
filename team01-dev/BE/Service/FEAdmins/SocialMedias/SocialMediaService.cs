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
using System.Threading.Tasks;

namespace Service.SocialMedias
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IRepositoryAsync<SocialMedia> _socialMediaRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public SocialMediaService(IRepositoryAsync<SocialMedia> socialmediaRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _socialMediaRepository = socialmediaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReturnMessage<SocialMediaDTO>> CreateAsync(CreateSocialMediaDTO model)
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
                _socialMediaRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<SocialMediaDTO>(false, _mapper.Map<SocialMedia, SocialMediaDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<SocialMediaDTO>> DeleteAsync(DeleteSocialMediaDTO model)
        {
            try
            {
                var entity = await _socialMediaRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    await _socialMediaRepository.DeleteAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
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

        public async Task<ReturnMessage<SocialMediaDTO>> UpdateAsync(UpdateSocialMediaDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            if (model.Title == null)
            {
                return new ReturnMessage<SocialMediaDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var entity = await _socialMediaRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _socialMediaRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
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

        public async Task<ReturnMessage<PaginatedList<SocialMediaDTO>>> SearchPaginationAsync(SearchPaginationDTO<SocialMediaDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<SocialMediaDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var resultEntity = await _socialMediaRepository.GetPaginatedListAsync(it => search.Search == null ||
                    (
                        (search.Search.Id != Guid.Empty && it.Id == search.Search.Id) ||
                        it.Title.Contains(search.Search.Title) ||
                        it.Link.Contains(search.Search.Link) ||
                        it.IconUrl.Contains(search.Search.IconUrl)
                    )
                 && !it.IsDeleted
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
