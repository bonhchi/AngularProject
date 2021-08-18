using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.PromotionDetails;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace Service.PromotionDetails
{
    public class PromotionDetailsService : IPromotionDetailsService
    {
        private readonly IRepositoryAsync<PromotionDetail> _promotionDetailRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public PromotionDetailsService(IRepositoryAsync<PromotionDetail> promotionDetailRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _promotionDetailRepository = promotionDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReturnMessage<PromotionDetailDTO>> CreateAsync(CreatePromotionDetailDTO model)
        {
            try
            {
                var entity = _mapper.Map<CreatePromotionDetailDTO, PromotionDetail>(model);
                entity.Insert();
                _promotionDetailRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<PromotionDetailDTO>(false, _mapper.Map<PromotionDetail, PromotionDetailDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDetailDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PromotionDetailDTO>> DeleteAsync(DeletePromotionDetailDTO model)
        {
            try
            {
                var entity = await _promotionDetailRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    entity.IsDeleted = true;
                    await _promotionDetailRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<PromotionDetailDTO>(false, _mapper.Map<PromotionDetail, PromotionDetailDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<PromotionDetailDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDetailDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PaginatedList<PromotionDetailDTO>>> SearchPaginationAsync(SearchPaginationDTO<PromotionDetailDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<PromotionDetailDTO>>(true, null, MessageConstants.CommonError);
            }

            var resultEntity = await _promotionDetailRepository.GetPaginatedListAsync(it => search.Search == null ||
                        search.Search.Id != Guid.Empty && it.Id == search.Search.Id
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => t.Value
            );
            var data = _mapper.Map<PaginatedList<PromotionDetail>, PaginatedList<PromotionDetailDTO>>(resultEntity);

            var result = new ReturnMessage<PaginatedList<PromotionDetailDTO>>(false, data, MessageConstants.ListSuccess);

            return result;
        }

        public async Task<ReturnMessage<PromotionDetailDTO>> UpdateAsync(UpdatePromotionDetailDTO model)
        {
            try
            {
                var entity = _promotionDetailRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _promotionDetailRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<PromotionDetailDTO>(false, _mapper.Map<PromotionDetail, PromotionDetailDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<PromotionDetailDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDetailDTO>(true, null, ex.Message);
            }
        }
    }
}
