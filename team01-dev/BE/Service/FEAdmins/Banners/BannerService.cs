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

namespace Service.Banners
{
    public class BannerService : IBannerService
    {
        private readonly IRepository<Banner> _bannerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public BannerService(IRepository<Banner> bannerRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserManager userManager)
        {
            _bannerRepository = bannerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public ReturnMessage<BannerDTO> Create(CreateBannerDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            if(model.Title == null)
            {
                var entity = _mapper.Map<CreateBannerDTO, Banner>(model);
                return new ReturnMessage<BannerDTO>(true, _mapper.Map<Banner, BannerDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var userInfo = _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<BannerDTO>(true, null, MessageConstants.CreateFail);
                }
                var entity = _mapper.Map<CreateBannerDTO, Banner>(model);
                entity.Insert(userInfo);
                _bannerRepository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<BannerDTO>(false, _mapper.Map<Banner, BannerDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BannerDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<BannerDTO> Delete(DeleteBannerDTO model)
        {
            try
            {
                var userInfo = _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<BannerDTO>(true, null, MessageConstants.CreateFail);
                }
                var entity = _bannerRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete(userInfo);
                    _bannerRepository.Delete(entity);
                    _unitOfWork.SaveChanges();
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
        public ReturnMessage<BannerDTO> Update(UpdateBannerDTO model)
        {

            model.Title = StringExtension.CleanString(model.Title);
            if (model.Title == null)
            {
                return new ReturnMessage<BannerDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var userInfo = _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<BannerDTO>(true, null, MessageConstants.CreateFail);
                }
                var entity = _bannerRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(userInfo, model);
                    _bannerRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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
        public ReturnMessage<PaginatedList<BannerDTO>> SearchPagination(SearchPaginationDTO<BannerDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<BannerDTO>>(false, null, MessageConstants.DeleteSuccess);
            }

            var resultEntity = _bannerRepository.GetPaginatedList(it => search.Search == null ||
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

