using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.Orders;
using Domain.DTOs.Products;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Service.Coupons;
using Service.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Coupon> _couponRepository;
        private readonly IRepository<Product> _productRepository;

        private readonly ICouponService _couponService;
        private readonly IProductService _productService;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IRepository<Product> productRepository, IProductService productService, ICouponService couponService, IRepository<Order> orderRepository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<Coupon> couponRepository)
        {

            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _couponRepository = couponRepository;
            _couponService = couponService;
            _productService = productService;
            _productRepository = productRepository;
        }

        public ReturnMessage<OrderDTO> Create(CreateOrderDTO model)
        {

            try
            {
                if(model.OrderDetails.IsNullOrEmpty())
                {
                    return new ReturnMessage<OrderDTO>(true, null, MessageConstants.Error);
                }
                var coupon = _couponRepository.Queryable().FirstOrDefault(t => t.Id == model.CouponId);
                if ((coupon.IsNotNullOrEmpty() && _couponService.GetByCode(coupon.Code).HasError == false) || model.CouponCode == null)
                {
                    var invalidProducts = false;
                    model.OrderDetails.ForEach(detail =>
                    {
                        var check = _productService.GetById(detail.ProductId);
                        if (check.HasError)
                        {
                            invalidProducts = true;
                        }
                    });

                    if (invalidProducts)
                    {
                        return new ReturnMessage<OrderDTO>(true, null, MessageConstants.Error);

                    }

                    var entity = _mapper.Map<CreateOrderDTO, Order>(model);
                    _unitOfWork.BeginTransaction();
                    entity.Insert(coupon);

                    _orderRepository.Insert(entity);

                    _unitOfWork.SaveChanges();
                    _unitOfWork.Commit();

                    var result = GetById(entity.Id);
                    return result;

                }
                return new ReturnMessage<OrderDTO>(true, null, MessageConstants.Error);

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<OrderDTO>(true, null, ex.Message);
            }
        }


        public ReturnMessage<OrderDTO> GetById(Guid id)
        {
            var order = _orderRepository.Queryable()
                .AsNoTracking()
                .Include(t => t.OrderDetails)
                .ThenInclude(t => t.Product)
                .FirstOrDefault(t => t.Id == id);
            var result = new ReturnMessage<OrderDTO>(false, _mapper.Map<Order, OrderDTO>(order), MessageConstants.GetPaginationSuccess);
            return result;

        }
        public ReturnMessage<OrderDTO> Delete(DeleteOrderDTO model)
        {
            try
            {
                var entity = _orderRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _orderRepository.Delete(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<OrderDTO>(false, _mapper.Map<Order, OrderDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<OrderDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<OrderDTO>(true, null, ex.Message);
            }
        }


        public ReturnMessage<PaginatedList<OrderDTO>> SearchPagination(SearchPaginationDTO<OrderDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<OrderDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var resultEntity = _orderRepository.GetPaginatedList(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.FullName.Contains(search.Search.FullName) ||
                        it.Code.Contains(search.Search.Code)
                    )
                )
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => t.CreateByDate
            );
            var data = _mapper.Map<PaginatedList<Order>, PaginatedList<OrderDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<OrderDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }

        public ReturnMessage<OrderDTO> Update(UpdateOrderDTO model)
        {
            try
            {
                var entity = _orderRepository.Queryable().AsNoTracking().Include(t => t.OrderDetails).FirstOrDefault(t=> t.Id == model.Id);
                if (entity.Status == CodeConstants.RejectedOrder)
                {
                    return new ReturnMessage<OrderDTO>(true, null, MessageConstants.UpdateFail);
                }
                if (entity.IsNotNullOrEmpty())
                {
                    if (model.Status == CodeConstants.ApprovedOrder && entity.Status== CodeConstants.NewOrder)
                    {
                        entity.Status = model.Status;
                        model = _mapper.Map<UpdateOrderDTO>(entity);
                        model.OrderDetails.ForEach(detail =>
                        {
                            var check = _productService.GetById(detail.ProductId);
                            if (!check.HasError)
                            {
                                var data = check.Data;
                                var dto = _mapper.Map<UpdateProductDTO>(data);
                                var update = _productService.UpdateCount(dto, detail.Quantity);
                            }
                        });
                    }
                    entity.Update(model);
                    _orderRepository.Update(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<OrderDTO>(false, _mapper.Map<Order, OrderDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<OrderDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<OrderDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<List<OrderDTO>> GetByStatus(string status)
        {
            try
            {
                var list = _orderRepository.Queryable().Where(t => t.Status == status).OrderByDescending(it => it.CreateByDate).ToList();
                if (list.IsNotNullOrEmpty())
                {
                    var result = _mapper.Map<List<OrderDTO>>(list);
                    return new ReturnMessage<List<OrderDTO>>(false, result, MessageConstants.ListSuccess);
                }
                return new ReturnMessage<List<OrderDTO>>(false, null, MessageConstants.Error);
            }
            catch(Exception ex)
            {
                return new ReturnMessage<List<OrderDTO>>(true, null, ex.Message);
            }
        }

    }
}
