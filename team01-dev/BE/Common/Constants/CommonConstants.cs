using Common.Enums;
using System;

namespace Common.Constants
{
    public struct CommonConstants
    {
        public static readonly Guid WebSiteInformationId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly string Address = "67 Tran Huy Lieu";
        public static readonly string Email = "email@gmail.com";
        public static readonly string Fax = "656565655656";
        public static readonly string Logo = "https://res.cloudinary.com/ngocphu/image/upload/v1622709208/vufymxnjiig3hno3ftnk.png"; //Thay logo
        public static readonly string Phone = "776767776767";
        public static readonly string Title = "Chào mừng đến với cửa hàng";
        public static readonly string Description = "Đây là trang thương mại điện tử";

    }
    public struct CommonConstantsUser
    {
        public static readonly Guid UserAdminId = Guid.Parse("10000000-0000-0000-0000-000000000000");
        public static readonly string UsernameAdmin = "admin";
        public static readonly string PasswordAdmin = MD5.MD5Helper.ToMD5Hash("123456");
        public static readonly UserType TypeAdmin = UserType.Admin;
    }

    public struct CommonConstantsProduct
    {
        public static readonly string Name = "Product ";
        public static readonly string Description = "Description for Product ";
        public static readonly string ContentHTML = "Content for Product ";
        public static readonly decimal Price = 1000000;
        public static readonly int DisplayOrder = 1;
        public static readonly string ImageUrl = "https://res.cloudinary.com/ngocphu/image/upload/v1622692452/dglsvo5idqneewby5qvh.jpg"; //Thay image
    }
    public struct CommonConstantsCategory
    {
        public static readonly string Name = "Category ";
        public static readonly string Description = "Description for Category ";
        public static readonly string ImageUrl = "https://res.cloudinary.com/ngocphu/image/upload/v1622706768/rl9ypusd3otwgvaclyva.png"; //Thay image
    }

    public struct CommonConstantsBlog
    {
        public static readonly string CreateByName = "admin";
    }

    public struct CommonConstantsComment
    {
        public static readonly string Minutes = " phút";
        public static readonly string Product = "Product";
        public static readonly int LimitTime = 15;
    }
}
