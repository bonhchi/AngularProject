using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.ProductsFeUser;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Service.Auth;
using Service.ProductDetailsFeUser;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ServiceFeUser
{
    public class ProductDetailsFeService : IProductDetailsFeService
    {
        private readonly IRepositoryAsync<Product> _productRepository; 
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public ProductDetailsFeService(IRepositoryAsync<Product> productRepository, IMapper mapper, IUserManager userManager)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _userManager = userManager;
            
        }
        public async Task<ReturnMessage<ProductDTOFeUser>> GetDetails(ProductDTOFeUser model)
        {
            if (model == null)
            {
                return new ReturnMessage<ProductDTOFeUser>(false, null, MessageConstants.Error);
            }
            var _userInformationDto = await _userManager.GetInformationUser();
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
