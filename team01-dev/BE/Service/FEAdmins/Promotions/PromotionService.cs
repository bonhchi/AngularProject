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

namespace Service.Promotions
{
    public class PromotionService : IPromotionService
    {
        private readonly IRepository<Promotion> _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PromotionService(IRepository<Promotion> promotionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ReturnMessage<PromotionDTO> Create(CreatePromotionDTO model)
        {
            try
            {
                var entity = _mapper.Map<CreatePromotionDTO, Promotion>(model);
                entity.Insert();
                _promotionRepository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<PromotionDTO>(false, _mapper.Map<Promotion, PromotionDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<PromotionDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PromotionDTO> Delete(DeletePromotionDTO model)
        {
            try
            {
                var entity = _promotionRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    entity.IsDeleted = true;
                    _promotionRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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

        public ReturnMessage<PromotionDTO> Update(UpdatePromotionDTO model)
        {
            try
            {
                var entity = _promotionRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    _promotionRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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
