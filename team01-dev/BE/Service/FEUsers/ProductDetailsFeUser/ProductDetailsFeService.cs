using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.ProductsFeUser;
using Domain.DTOs.Users;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Service.Auth;
using Service.ProductDetailsFeUser;
using System.Linq;

namespace Service.ServiceFeUser
{
    public class ProductDetailsFeService : IProductDetailsFeService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private UserInformationDTO _userInformationDto;
        private readonly IUserManager _userManager;

        public ProductDetailsFeService(IRepository<Category> categoryRepository, IRepository<Product> productRepository, IMapper mapper, IUserManager userManager)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _userInformationDto = _userManager.GetInformationUser();
        }
        public ReturnMessage<ProductDTOFeUser> GetDetails(ProductDTOFeUser model)
        {
            if (model == null)
            {
                return new ReturnMessage<ProductDTOFeUser>(false, null, MessageConstants.Error);
            }

            var resultEntity = _productRepository
                .Queryable()
                .Include(t => t.Category).Include(t => t.CustomerWishLists)
                .Where(it => !it.IsDeleted && it.Id == model.Id).FirstOrDefault();
            var data = _mapper.Map<Product, ProductDTOFeUser>(resultEntity);
            data.IsInWishList = resultEntity.CustomerWishLists.IsNotNullOrEmpty() && resultEntity.CustomerWishLists.Any(k => k.CustomerId == _userInformationDto.CustomerId);
            var result = new ReturnMessage<ProductDTOFeUser>(false, data, MessageConstants.ListSuccess);
            return result;
        }
        
    }
}
