using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Constants
{
    public struct CommonConstants
    {
        public static Guid WebSiteInformationId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static string Address = "67 Tran Huy Lieu";
        public static string Email = "email@gmail.com";
        public static string Fax = "656565655656";
        public static string Logo = "https://res.cloudinary.com/tungimage/image/upload/v1620372703/ridn0zmlny900h2sjlmh.png"; //Thay logo
        public static string Phone = "776767776767";
        public static string Title = "Welcome to Our ShopDbTung Store";
        public static string Description = "This is a page for fashion";

    }
    public struct CommonConstantsUser
    {
        public static Guid UserAdminId = Guid.Parse("10000000-0000-0000-0000-000000000000");
        public static string UsernameAdmin = "admin";
        public static string PasswordAdmin = MD5.MD5Helper.ToMD5Hash("123456");
        public static UserType TypeAdmin = UserType.Admin;
    }

    public struct CommonConstantsProduct
    {
        public static string Name = "Product ";
        public static string Description = "Description for Product ";
        public static string ContentHTML = "Content for Product ";
        public static decimal Price = 1000000;
        public static int DisplayOrder = 1;
        public static string ImageUrl = "https://res.cloudinary.com/tungimage/image/upload/v1620233950/pyv3241siykegow7phjf.jpg"; //Thay image
    }
    public struct CommonConstantsCategory
    {
        public static string Name = "Category ";
        public static string Description = "Description for Category ";
        public static string ImageUrl = "https://res.cloudinary.com/tungimage/image/upload/v1620233950/pyv3241siykegow7phjf.jpg"; //Thay image
    }

    public struct CommonConstantsBlog
    {
        public static string CreateByName = "admin";
    }

    public struct CommonConstantsComment
    {
        public static string Minutes = " phút";
        public static string Product = "Product";
        public static int LimitTime = 15;
    }
}
