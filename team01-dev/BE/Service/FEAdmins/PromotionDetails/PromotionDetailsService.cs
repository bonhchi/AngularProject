using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.PromotionDetails;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PromotionDetails
{
    public class PromotionDetailsService : IPromotionDetailsService
    {
        private readonly IRepository<PromotionDetail> _promotionDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PromotionDetailsService(IRepository<PromotionDetail> promotionDetailRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _promotionDetailRepository = promotionDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ReturnMessage<PromotionDetailDTO> Create(CreatePromotionDetailDTO model)
        {
            try
            {
                var entity = _mapper.Map<CreatePromotionDetailDTO, PromotionDetail>(model);
                entity.Insert();
                _promotionDetailRepository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<PromotionDetailDTO>(false, _mapper.Map<PromotionDetail, PromotionDetailDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDetailDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PromotionDetailDTO> Delete(DeletePromotionDetailDTO model)
        {
            try
            {
                var entity = _promotionDetailRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    entity.IsDeleted = true;
                    _promotionDetailRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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

        public ReturnMessage<PaginatedList<PromotionDetailDTO>> SearchPagination(SearchPaginationDTO<PromotionDetailDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<PromotionDetailDTO>>(true, null, MessageConstants.CommonError);
            }

            var resultEntity = _promotionDetailRepository.GetPaginatedList(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id)
                    )
                )
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => t.Value
            );
            var data = _mapper.Map<PaginatedList<PromotionDetail>, PaginatedList<PromotionDetailDTO>>(resultEntity);

            var result = new ReturnMessage<PaginatedList<PromotionDetailDTO>>(false, data, MessageConstants.ListSuccess);

            return result;
        }

        public ReturnMessage<PromotionDetailDTO> Update(UpdatePromotionDetailDTO model)
        {
            try
            {
                var entity = _promotionDetailRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    _promotionDetailRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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
