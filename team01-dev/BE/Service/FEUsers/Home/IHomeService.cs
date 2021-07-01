using Common.Http;
using Domain.DTOs.Banners;
using Domain.DTOs.Blogs;
using Domain.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Home
{
    public interface IHomeService
    {
        public Task<ReturnMessage<List<ProductDTO>>> GetTopCollectionProducts();
        public Task<ReturnMessage<List<ProductDTO>>> GetNewProducts();
        public Task<ReturnMessage<List<ProductDTO>>> GetBestSellerProducts();
        public Task<ReturnMessage<List<ProductDTO>>> GetFeaturedProducts();
        public Task<ReturnMessage<List<BlogDTO>>> GetBlogs();
        public Task<ReturnMessage<List<BannerDTO>>> GetBanners();
    }
}
