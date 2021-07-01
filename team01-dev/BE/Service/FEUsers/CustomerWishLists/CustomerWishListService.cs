using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.CustomerWishList;
using Domain.DTOs.Products;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Service.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.CustomerWishLists
{
    public class CustomerWishListService : ICustomerWishListService
    {
        private readonly IRepositoryAsync<CustomerWishList> _wishListRepository;
        private readonly IRepositoryAsync<Product> _productRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public CustomerWishListService(IRepositoryAsync<CustomerWishList> wishListRepository, IRepositoryAsync<Product> productRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper, IUserManager userManager)
        {
            _wishListRepository = wishListRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReturnMessage<CustomerWishListDTO>> CreateOrDelete(CreateOrDeleteCustomerWishListDTO model)
        {
            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.CustomerId.IsValid())
                {
                    var checkExistEntity = _wishListRepository.Queryable()
                                            .FirstOrDefault(i => i.CustomerId == userInfo.CustomerId && i.ProductId == model.ProductId);

                    if (checkExistEntity.IsNotNullOrEmpty())
                    {
                        checkExistEntity.Delete();
                        _wishListRepository.Delete(checkExistEntity);
                        await _unitOfWork.SaveChangesAsync();
                        var _result = _mapper.Map<CustomerWishList, CustomerWishListDTO>(checkExistEntity);
                        return new ReturnMessage<CustomerWishListDTO>(false, _result, MessageConstants.DeleteSuccess);
                    }

                    var entity = _mapper.Map<CreateOrDeleteCustomerWishListDTO, CustomerWishList>(model);
                    entity.CustomerId = userInfo.CustomerId.GetValueOrDefault();
                    entity.Insert();
                    _wishListRepository.Insert(entity);
                    _unitOfWork.SaveChangesAsync();

                    var result = _mapper.Map<CustomerWishList, CustomerWishListDTO>(entity);
                    return new ReturnMessage<CustomerWishListDTO>(false, result, MessageConstants.CreateSuccess);
                }
                return new ReturnMessage<CustomerWishListDTO>(false, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CustomerWishListDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<List<ProductDTO>>> GetByCustomer()
        {
            try
            {
                var customer = await _userManager.GetInformationUser();

                var listProductId =  _wishListRepository.Queryable()
                                    .Where(i => i.CustomerId == customer.CustomerId)
                                    .Select(i => i.ProductId)
                                    .ToList();

                var listProduct = _productRepository.Queryable()
                                    .Where(i => listProductId.Any(p => p == i.Id))
                                    .OrderByDescending(i => i.CreateByDate)
                                    .ToList();

                var data = _mapper.Map<List<Product>, List<ProductDTO>>(listProduct);

                var result = new ReturnMessage<List<ProductDTO>>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, MessageConstants.Error);
            }
        }
    }
}
