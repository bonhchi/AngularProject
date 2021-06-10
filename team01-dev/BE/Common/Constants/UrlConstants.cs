using System.IO;

namespace Common.Constants
{
    public struct UrlConstants
    {
        public const string Host = "https://localhost:44309";

        public static string BaseLocalUrlFile = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files");
        public static string BaseCloudUrlFile = Path.Combine(Host, "Files");

        public const string BaseApi = "/api";

        public const string BaseSupplier = BaseApi + "/supplier";

        public const string BaseSocialMedia = BaseApi + "/social-media";

        public const string BaseAuth = BaseApi + "/auth";
        public const string BaseAuthCustomer = BaseApi + "/user/auth";

        public const string BaseBanner = BaseApi + "/banner";

        public const string BaseLogin = BaseAuth + "/login";
        public const string BaseLoginCustomer = BaseAuthCustomer + "/login";

        public const string BaseRegistCustomer = BaseAuthCustomer + "/regist";

        public const string BaseProfile = BaseApi + "/profile";
        public const string BaseProfileCustomer = BaseApi + "/user/profile";

        public const string BaseCategory = BaseApi + "/category";

        public const string BaseFile = BaseApi + "/file";

        public const string BaseFileDownload = BaseFile + "/download";

        public const string BaseFileGetType = BaseFile + "/type";

        public const string BaseCoupon = BaseApi + "/coupon";
        public const string CouponCode = "by-code";




        public const string BaseUser = BaseApi + "/user";
        public const string GetUser = BaseUser + "/{id}";

        public const string BaseCustomer = BaseApi + "/customer";

        public const string BaseProduct = BaseApi + "/product";

        public const string BaseHeader = BaseApi + "/header";
        public const string BaseFooter = BaseApi + "/footer";

        public const string BaseProductDetailsFeUser = BaseApi + "/user/product-details";



        public const string BaseBlog = BaseApi + "/blog";
        public const string BaseOrder = BaseApi + "/order";
        public const string BaseOrderDetail = BaseApi + "/order-detail";
        public const string OrderStatus = "by-status";
        public const string OrderId = "by-id";



        public const string BaseProductList = BaseApi + "/user/productlist";

        public const string Product = "product";
        public const string Category = "category";
        public const string ByCategory = "by-category";
        public const string RevelantProduct = "relevant";

        public const string BaseHome = BaseApi + "/home";

        public const string TopCollection = "top-collection";
        public const string NewProducts = "new-products";
        public const string BestSeller = "best-seller";
        public const string FeaturedProducts = "featured-products";
        public const string OnSale = "on-sale";
        public const string Blogs = "blogs";
        public const string Banners = "banners";
        public const string Password = "password";
        public const string CheckEmail = "checkemail";
        public const string CheckPhone = "checkphone";
        public const string CheckUserName = "checkusername";

        public const string TopBlog = BaseBlog + "/topblog";
        public const string RecentBlog = BaseBlog + "/recentblog";
        public const string GetBlog = BaseBlog + "/{id}";
        public const string GetUserBlog = BaseBlog + "/user";

        public const string BaseInformationWebsite = BaseApi + "/info-website";
        public const string BaseUserInformationWebsite = BaseUser + "/info-website";

        // Page Content
        public const string BasePageContent = BaseApi + "/page-content";
        public const string GetPageContent = BaseApi + "/page-content/{id}";

        //User Page Content
        public const string BaseUserPageContent = BaseUser + "/page-content";
        public const string GetPageContentUser = BaseUser + "/page-content/{id}";

        // Contact
        public const string BaseContact = BaseApi + "/contact";

        //User Contact
        public const string BaseUserContact = BaseUser + "/contact";

        // Comment
        public const string BaseComment = BaseApi + "/comment";
        public const string GetCommentBlog = BaseComment + "/blog";
        public const string GetCommentProduct= BaseComment + "/product";
        // CustomerWishList
        public const string BaseCustomerWishList = BaseApi + "/customer-wish-list";
        public const string GetCustomerWishList = BaseApi + "/customer-wish-list/{customerId}";

        public const string BasePromotion = BaseApi + "/promotion";

        public const string BasePromotionDetail = BaseApi + "/promotion-detail";

        


    }
}
