using Common.Http;
using Domain.DTOs.Banners;
using Domain.DTOs.Blogs;
using Domain.DTOs.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Home
{
    public interface IHomeService
    {
        public Task<ReturnMessage<List<SubcategoryDTO>>> GetTopCollectionProducts();
        public Task<ReturnMessage<List<SubcategoryDTO>>> GetNewProducts();
        public Task<ReturnMessage<List<SubcategoryDTO>>> GetBestSellerProducts();
        public Task<ReturnMessage<List<SubcategoryDTO>>> GetFeaturedProducts();
        public Task<ReturnMessage<List<BlogDTO>>> GetBlogs();
        public Task<ReturnMessage<List<BannerDTO>>> GetBanners();
    }
}
