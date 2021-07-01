using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Promotions;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Promotions
{
    public class PromotionService : IPromotionService
    {
        private readonly IRepositoryAsync<Promotion> _promotionRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public PromotionService(IRepositoryAsync<Promotion> promotionRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReturnMessage<PromotionDTO>> CreateAsync(CreatePromotionDTO model)
        {
            try
            {
                var entity = _mapper.Map<CreatePromotionDTO, Promotion>(model);
                entity.Insert();
                _promotionRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<PromotionDTO>(false, _mapper.Map<Promotion, PromotionDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PromotionDTO>> DeleteAsync(DeletePromotionDTO model)
        {
            try
            {
                var entity = await _promotionRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    entity.IsDeleted = true;
                    _promotionRepository.Update(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<PromotionDTO>(false, _mapper.Map<Promotion, PromotionDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<PromotionDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PaginatedList<PromotionDTO>> SearchPagination(SearchPaginationDTO<PromotionDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<PromotionDTO>>(true, null, MessageConstants.Error);
            }

            var resultEntity = _promotionRepository.GetPaginatedList(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.Title.Contains(search.Search.Title) ||
                        it.Description.Contains(search.Search.Description)
                    )
                )
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => t.Title
            );
            var data = _mapper.Map<PaginatedList<Promotion>, PaginatedList<PromotionDTO>>(resultEntity);

            var result = new ReturnMessage<PaginatedList<PromotionDTO>>(false, data, MessageConstants.ListSuccess);

            return result;
        }

        public async Task<ReturnMessage<PromotionDTO>> UpdateAsync(UpdatePromotionDTO model)
        {
            try
            {
                var entity = await _promotionRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _promotionRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<PromotionDTO>(false, _mapper.Map<Promotion, PromotionDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<PromotionDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDTO>(true, null, ex.Message);
            }
        }
    }
}
