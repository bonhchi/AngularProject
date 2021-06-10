using Common.Http;
using Domain.DTOs.Banners;
using Domain.DTOs.Blogs;
using Domain.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Home
{
    public interface IHomeService
    {
        public ReturnMessage<List<ProductDTO>> GetTopCollectionProducts();
        public ReturnMessage<List<ProductDTO>> GetNewProducts();
        public ReturnMessage<List<ProductDTO>> GetBestSellerProducts();
        public ReturnMessage<List<ProductDTO>> GetFeaturedProducts();
        public ReturnMessage<List<BlogDTO>> GetBlogs();
        public ReturnMessage<List<BannerDTO>> GetBanners();
    }
}
