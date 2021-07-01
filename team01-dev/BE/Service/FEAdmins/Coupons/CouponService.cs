using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.Coupons;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Service.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly IRepositoryAsync<Coupon> _couponRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public CouponService(IRepositoryAsync<Coupon> couponRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReturnMessage<CouponDTO>> CreateAsync(CreateCouponDTO model)
        {
            model.Code = StringExtension.CleanString(model.Code);
            model.Name = StringExtension.CleanString(model.Name);
            if (model.Code == null || model.Name == null)
            {
                var entity = _mapper.Map<CreateCouponDTO, Coupon>(model);
                return new ReturnMessage<CouponDTO>(true, _mapper.Map<Coupon, CouponDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var entity = _mapper.Map<CreateCouponDTO, Coupon>(model);
                if (DateTime.Compare(entity.StartDate, entity.EndDate) < 0)
                {
                    entity.Insert();
                    _couponRepository.InsertAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<CouponDTO>(false, _mapper.Map<Coupon, CouponDTO>(entity), MessageConstants.CreateSuccess);
                    return result;
                }
                return new ReturnMessage<CouponDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CouponDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<CouponDTO>> DeleteAsync(DeleteCouponDTO model)
        {
            try
            {
                var entity = await _couponRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    await _couponRepository.DeleteAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<CouponDTO>(false, _mapper.Map<Coupon, CouponDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<CouponDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CouponDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<CouponDTO>> UpdateAsync(UpdateCouponDTO model)
        {
            model.Code = StringExtension.CleanString(model.Code);
            model.Name = StringExtension.CleanString(model.Name);
            if (model.Code == null || model.Name == null)
            {
                var entity = _mapper.Map<UpdateCouponDTO, Coupon>(model);
                return new ReturnMessage<CouponDTO>(true, _mapper.Map<Coupon, CouponDTO>(entity), MessageConstants.UpdateFail);
            }
            try
            {
                var entity = await _couponRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty() || DateTime.Compare(entity.StartDate, entity.EndDate) < 0 || DateTime.Compare(DateTime.Now , entity.StartDate) < 0)
                {
                    entity.Update(model);
                    await _couponRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<CouponDTO>(false, _mapper.Map<Coupon, CouponDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;

                }
                return new ReturnMessage<CouponDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CouponDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PaginatedList<CouponDTO>> SearchPagination(SearchPaginationDTO<CouponDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<CouponDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var resultEntity = _couponRepository.GetPaginatedList(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.Code.Contains(search.Search.Code) ||
                        it.Name.Contains(search.Search.Name)
                    )
                ) && !it.IsDeleted
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => t.StartDate
            );
            var data = _mapper.Map<PaginatedList<Coupon>, PaginatedList<CouponDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<CouponDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }


        public ReturnMessage<CouponDTO> GetByCode(string code)
        {
            var entity = _couponRepository.Queryable().FirstOrDefault(t => t.Code == code);
            if (entity.IsNotNullOrEmpty())
            {
                if(entity.EndDate < DateTime.Now)
                {
                    return new ReturnMessage<CouponDTO>(true, null, MessageConstants.Error);

                }
                var result = _mapper.Map<CouponDTO>(entity);
                return new ReturnMessage<CouponDTO>(false, result, MessageConstants.GetSuccess);
            }

            return new ReturnMessage<CouponDTO>(true, null, MessageConstants.Error);

        }
    }
}
