using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Banners;
using Domain.DTOs.Blogs;
using Domain.DTOs.Products;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Service.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Home
{
    public class HomeService : IHomeService
    {
        private readonly IRepositoryAsync<Product> _productRepository;
        private readonly IRepositoryAsync<Blog> _blogRepository;
        private readonly IRepositoryAsync<Banner> _bannerRepository;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        //private UserInformationDTO _userInformationDto;

        public HomeService(IRepositoryAsync<Product> productRepository, IUserManager userManager, IRepositoryAsync<Blog> blogRepository, IRepositoryAsync<Banner> bannerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _bannerRepository = bannerRepository;
            _blogRepository = blogRepository;
            _bannerRepository = bannerRepository;
            _mapper = mapper;
            _userManager = userManager;
            /*_userInformationDto = _userManager.GetInformationUser();*/ // not use
        }

        public async Task<ReturnMessage<List<ProductDTO>>> GetTopCollectionProducts()
        {
            try
            {
                var _userInformationDto = await _userManager.GetInformationUser();
                var resultEntity = _productRepository.Queryable()
                                    .Include(t => t.Category)
                                    .Include(t => t.CustomerWishLists)
                                    .Where(i => i.IsDeleted == false)
                                    .OrderByDescending(i => i.RatingScore)
                                    .ThenByDescending(i => i.UpdateByDate)
                                    .Take(12)
                                    .ToList();
                var data = _mapper.Map<List<Product>, List<ProductDTO>>(resultEntity);

                data.ForEach(t => t.IsInWishList = t.CustomerWishLists.IsNotNullOrEmpty() && t.CustomerWishLists.Any(k => k.CustomerId == _userInformationDto.CustomerId));

                var result = new ReturnMessage<List<ProductDTO>>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, MessageConstants.Error);
            }
        }

        public async Task<ReturnMessage<List<ProductDTO>>> GetNewProducts()
        {
            try
            {
                var _userInformationDto = await _userManager.GetInformationUser();
                var resultEntity = _productRepository.Queryable()
                                    .Include(t => t.Category)
                                    .Include(t => t.CustomerWishLists)
                                    .Where(i => i.IsDeleted == false)
                                    .OrderByDescending(i => i.HasDisplayHomePage)
                                    .ThenByDescending(i => i.CreateByDate)
                                    .Take(12)
                                    .ToList();
                var data = _mapper.Map<List<Product>, List<ProductDTO>>(resultEntity);

                data.ForEach(t => t.IsInWishList = t.CustomerWishLists.IsNotNullOrEmpty() && t.CustomerWishLists.Any(k => k.CustomerId == _userInformationDto.CustomerId));

                var result = new ReturnMessage<List<ProductDTO>>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, MessageConstants.Error);
            }
        }

        public async Task<ReturnMessage<List<ProductDTO>>> GetBestSellerProducts()
        {
            var _userInformationDto = await _userManager.GetInformationUser();
            try
            {
                var resultEntity = _productRepository.Queryable()
                                    .Include(t => t.Category)
                                    .Include(t => t.CustomerWishLists)
                                    .Where(i => i.IsDeleted == false)
                                    .OrderByDescending(i => i.SaleCount)
                                    .ThenByDescending(i => i.UpdateByDate)
                                    .Take(12)
                                    .ToList();
                var data = _mapper.Map<List<Product>, List<ProductDTO>>(resultEntity);

                data.ForEach(t => t.IsInWishList = t.CustomerWishLists.IsNotNullOrEmpty() && t.CustomerWishLists.Any(k => k.CustomerId == _userInformationDto.CustomerId));

                var result = new ReturnMessage<List<ProductDTO>>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, MessageConstants.Error);
            }
        }

        public async Task<ReturnMessage<List<ProductDTO>>> GetFeaturedProducts()
        {
            try
            {
                var _userInformationDto = await _userManager.GetInformationUser();
                var resultEntity = _productRepository.Queryable()
                                    .AsQueryable()
                                    .Include(t => t.Category)
                                    .Include(t => t.CustomerWishLists)
                                    .Where(i => i.IsDeleted == false)
                                    .Where(i => i.IsFeatured == true)
                                    .OrderByDescending(i => i.DisplayOrder)
                                    .ThenByDescending(i => i.UpdateByDate)
                                    .Take(12)
                                    .ToList();
                var data = _mapper.Map<List<Product>, List<ProductDTO>>(resultEntity);
                data.ForEach(t => t.IsInWishList = t.CustomerWishLists.IsNotNullOrEmpty() && t.CustomerWishLists.Any(k => k.CustomerId == _userInformationDto.CustomerId));

                var result = new ReturnMessage<List<ProductDTO>>(false, data, MessageConstants.ListSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, MessageConstants.Error);
            }
        }

        //async missing
        public async Task<ReturnMessage<List<BlogDTO>>> GetBlogs()
        {
            try
            {
                var resultEntity =  _blogRepository.Queryable().Where(i => i.IsDeleted == false).OrderByDescending(it => it.UpdateByDate).Take(12).ToList();
                var data = _mapper.Map<List<Blog>, List<BlogDTO>>(resultEntity);
                var result = new ReturnMessage<List<BlogDTO>>(false, data, MessageConstants.ListSuccess);
                await Task.CompletedTask;
                return result;
            }
            catch
            {
                return new ReturnMessage<List<BlogDTO>>(true, null, MessageConstants.Error);
            }
        }
        //async missing
        public async Task<ReturnMessage<List<BannerDTO>>> GetBanners()
        {
            try
            {
                var resultEntity = _bannerRepository.Queryable().Where(i => i.IsDeleted == false).Take(12).OrderBy(it => it.DisplayOrder).ToList();
                var data = _mapper.Map<List<Banner>, List<BannerDTO>>(resultEntity);
                var result = new ReturnMessage<List<BannerDTO>>(false, data, MessageConstants.ListSuccess);
                await Task.CompletedTask;
                return result;
            }
            catch
            {
                return new ReturnMessage<List<BannerDTO>>(true, null, MessageConstants.Error);
            }
        }
    }
}
