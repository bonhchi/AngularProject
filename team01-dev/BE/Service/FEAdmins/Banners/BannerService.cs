using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.Banners;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Service.Auth;
using System;
using System.Threading.Tasks;

namespace Service.Banners
{
    public class BannerService : IBannerService
    {
        private readonly IRepositoryAsync<Banner> _bannerRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public BannerService(IRepositoryAsync<Banner> bannerRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper, IUserManager userManager)
        {
            _bannerRepository = bannerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReturnMessage<BannerDTO>> CreateAsync(CreateBannerDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            if(model.Title == null)
            {
                var entity = _mapper.Map<CreateBannerDTO, Banner>(model);
                return new ReturnMessage<BannerDTO>(true, _mapper.Map<Banner, BannerDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<BannerDTO>(true, null, MessageConstants.CreateFail);
                }
                var entity = _mapper.Map<CreateBannerDTO, Banner>(model);
                entity.Insert(userInfo);
                _bannerRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<BannerDTO>(false, _mapper.Map<Banner, BannerDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BannerDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<BannerDTO>> DeleteAsync(DeleteBannerDTO model)
        {
            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<BannerDTO>(true, null, MessageConstants.CreateFail);
                }
                var entity = await _bannerRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete(userInfo);
                    _bannerRepository.Delete(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<BannerDTO>(false, _mapper.Map<Banner, BannerDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<BannerDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BannerDTO>(true, null, ex.Message);
            }
        }
        public async Task<ReturnMessage<BannerDTO>> UpdateAsync(UpdateBannerDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            if (model.Title == null)
            {
                return new ReturnMessage<BannerDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<BannerDTO>(true, null, MessageConstants.CreateFail);
                }
                var entity = await _bannerRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(userInfo, model);
                    await _bannerRepository.UpdateAsync(entity); //test flow
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<BannerDTO>(false, _mapper.Map<Banner, BannerDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<BannerDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BannerDTO>(true, null, ex.Message);
            }
        }
        public async Task<ReturnMessage<PaginatedList<BannerDTO>>> SearchPagination(SearchPaginationDTO<BannerDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<BannerDTO>>(false, null, MessageConstants.DeleteSuccess);
            }

            var resultEntity = await _bannerRepository.GetPaginatedListAsync(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.Title.Contains(search.Search.Title) ||
                        it.Description.Contains(search.Search.Description)
                    )
                )
                , search.PageSize
                , search.PageIndex
                , t => -t.DisplayOrder
            );
            var data = _mapper.Map<PaginatedList<Banner>, PaginatedList<BannerDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<BannerDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }


    }
}

