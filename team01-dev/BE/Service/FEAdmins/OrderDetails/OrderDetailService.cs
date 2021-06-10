using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.OrderDetails;
using Domain.DTOs.Orders;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.OrderDetails
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderDetailService(IRepository<OrderDetail> orderDetailRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ReturnMessage<OrderDetailDTO> Create(CreateOrderDetailDTO model)
        {

                return new ReturnMessage<OrderDetailDTO>(false, null, null);
        }

        public ReturnMessage<OrderDetailDTO> Delete(DeleteOrderDetailDTO model)
            {
            try
            {
                var entity = _orderDetailRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _orderDetailRepository.Delete(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<OrderDetailDTO>(false, _mapper.Map<OrderDetail, OrderDetailDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<OrderDetailDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<OrderDetailDTO>(true, null, ex.Message);
        }
    }


    public ReturnMessage<PaginatedList<OrderDetailDTO>> SearchPagination(SearchPaginationDTO<OrderDetailDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<OrderDetailDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var query = _orderDetailRepository.Queryable().Include(it => it.Product).Where(it => search.Search == null ||
                    (
                        (
                            (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id)
                        )
                    )
                )
                .OrderBy(it => it.Product.Name)
                .ThenBy(it => it.Product.Name.Length);
            var resultEntity = new PaginatedList<OrderDetail>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<OrderDetail>, PaginatedList<OrderDetailDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<OrderDetailDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }

        public ReturnMessage<OrderDetailDTO> Update(UpdateOrderDetailDTO model)
        {
            try
            {
                var entity = _orderDetailRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    _orderDetailRepository.Update(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<OrderDetailDTO>(false, _mapper.Map<OrderDetail, OrderDetailDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<OrderDetailDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<OrderDetailDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<List<OrderDetailDTO>> GetByOrder(Guid Id)
        {
            var entity = _orderDetailRepository.Queryable().AsNoTracking().Include(t=>t.Product).Where(t => t.OrderId == Id).ToList();
            var result = new ReturnMessage<List<OrderDetailDTO>>(false, _mapper.Map<List<OrderDetail>, List<OrderDetailDTO>>(entity), MessageConstants.ListSuccess);
            return result;

        }


    }
}
