using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.OrderDetails;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OrderDetails
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepositoryAsync<OrderDetail> _orderDetailRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        public OrderDetailService(IRepositoryAsync<OrderDetail> orderDetailRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //not use
        public async Task<ReturnMessage<OrderDetailDTO>> CreateAsync(CreateOrderDetailDTO model)
        {
            await Task.CompletedTask;
            return new ReturnMessage<OrderDetailDTO>(false, null, null);
        }

        public async Task<ReturnMessage<OrderDetailDTO>> DeleteAsync(DeleteOrderDetailDTO model)
            {
            try
            {
                var entity = await _orderDetailRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    await _orderDetailRepository.DeleteAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
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


    public async Task<ReturnMessage<PaginatedList<OrderDetailDTO>>> SearchPaginationAsync(SearchPaginationDTO<OrderDetailDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<OrderDetailDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var query = _orderDetailRepository.Queryable().Include(it => it.Product).Where(it => search.Search == null ||
                    (
                        (
                            (search.Search.Id != Guid.Empty && it.Id == search.Search.Id)
                        )
                    )
                )
                .OrderBy(it => it.Product.Name)
                .ThenBy(it => it.Product.Name.Length);
            var resultEntity = new PaginatedList<OrderDetail>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<OrderDetail>, PaginatedList<OrderDetailDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<OrderDetailDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            await Task.CompletedTask;
            return result;
        }

        public async Task<ReturnMessage<OrderDetailDTO>> UpdateAsync(UpdateOrderDetailDTO model)
        {
            try
            {
                var entity = await _orderDetailRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _orderDetailRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
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

        public async Task<ReturnMessage<List<OrderDetailDTO>>> GetByOrder(Guid Id)
        {
            var entity = _orderDetailRepository.Queryable().AsNoTracking().Include(t=>t.Product).Where(t => t.OrderId == Id).ToList();
            var result = new ReturnMessage<List<OrderDetailDTO>>(false, _mapper.Map<List<OrderDetail>, List<OrderDetailDTO>>(entity), MessageConstants.ListSuccess);
            await Task.CompletedTask;
            return result;

        }


    }
}
