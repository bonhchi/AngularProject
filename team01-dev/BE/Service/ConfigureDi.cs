using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Factories;
using Microsoft.Extensions.DependencyInjection;
using Service.SocialMedias;
using Service.Categories;
using Service.Banners;
using Service.Auth;
using Service.Files;
using Service.Coupons;
using Service.Profiles;
using Service.Users;
using Service.Blogs;
using Service.Products;
using Service.ProductDetailsFeUser;
using Service.ServiceFeUser;
using Service.Home;
using Service.Header;
using Service.Footer;
using Service.Orders;
using Service.UserProductList;
using Service.Customers;
using Service.AuthCustomer;
using Service.PageContents;
using Service.InformationWebsiteServices;
using Service.CustomerProfileFeUser;
using Service.OrderDetails;
using Service.Contacts;
using Service.Comments;
using Service.CustomerWishLists;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Service.UserBlogs;
using Service.Gmails;
using Service.UserPageContents;

namespace Service
{
    public static class ConfigureDi
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<RepositoryFactories>();
            services.AddScoped<IRepositoryProvider, RepositoryProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkAsync, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICouponService, CouponService>();
            //scoped
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<IAuthService, AuthService>();
            //scoped
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileManager, FileManager>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<IFooterService, FooterService>();


            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IOrderService, OrderService>();


            services.AddScoped<IHomeService, HomeService>();

            //Customer
            services.AddScoped<ICustomerService, CustomerService>();

            ////AuthCustomer
            services.AddScoped<IAuthCustomerUserService, AuthCustomerUserService>();

            //ProfileCustomer
            services.AddScoped<ICustomerProfileFeUserService, CustomerProfileFeUserService>();

            //FeUser

            services.AddScoped<IUserProductListService, UserProductListService>();

            services.AddScoped<IProductDetailsFeService, ProductDetailsFeService>();
            services.AddScoped<IPageContentService,PageContentService>();


            //Information Website
            services.AddScoped<IInfomationWebService, InformationWebService>();

            //Order Details
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            // Contact
            services.AddScoped<IContactService, ContactService>();

            // Comment 
            services.AddScoped<ICommentService, CommentService>();

            // CustomerWishList
            services.AddScoped<ICustomerWishListService, CustomerWishListService>();

            //BlogUser
            services.AddScoped<IUserBlogService, UserBlogService>();

            //Gmail
            services.AddScoped<IGmailService, GmailService>();

            // User page content
            services.AddScoped<IUserPageContentService, UserPageContentService>();
        }
    }
}
