using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Constants
{
    public struct SubcategoryTypeConstants
    {
        public static readonly Guid SubcategoryBrand = new("00000000-0000-0000-0000-000000000001");
        public static readonly Guid SubcategoryType = new("00000000-0000-0000-0000-000000000002");
        public static readonly Guid SubcategoryPriceTag = new("00000000-0000-0000-0000-000000000003");
        public static readonly Guid SubcategoryProductList = new("00000000-0000-0000-0000-000000000004");
        public static readonly Guid SubcategorySeries = new("00000000-0000-0000-0000-000000000005");

        public static readonly Dictionary<Guid, SubcategoryType> ListSubcategoryType = new()
        {
            {
                SubcategoryBrand,
                new SubcategoryType()
                {
                    Name = "Thương hiệu",
                    EnglishName = "Brand",
                }
            },
            {
                SubcategoryType,
                new SubcategoryType()
                {
                    Name = "Thể loại",
                    EnglishName = "Type",
                }
            },
            {
                SubcategoryPriceTag,
                new SubcategoryType()
                {
                    Name = "Mức giá",
                    EnglishName = "Pricetag",
                }
            },
            {
                SubcategoryProductList,
                new SubcategoryType()
                {
                    Name = "Danh sách sản phẩm",
                    EnglishName = "Productlist",
                }
            },
            {
                SubcategorySeries,
                new SubcategoryType()
                {
                    Name = "Bộ sưu tập",
                    EnglishName = "Series",
                }
            }
        };
    }
}
