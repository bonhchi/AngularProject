using System;
using System.Collections.Generic;
namespace Common.Constants
{
    public struct DataType
    {
        public static Dictionary<string, string> TypeName = new()
        {
            {"banner", "BAN" },
            {"category", "CATE" },
            {"file", "FILE" },
            {"product", "PRO" },
            {"socialmedia", "SOC" },
            {"user", "USER" },
            {"customer", "CUSTOMER"},
            { "blog", "BLOG" }
        };

        public enum ETypeFile
        {
            Image,
            File
        }

        public static Dictionary<string, ETypeFile> TypeFile = new()
        {
            { "BAN", ETypeFile.Image },
            { "CATE", ETypeFile.Image },
            { "FILE",ETypeFile.File },
            { "PRO", ETypeFile.Image },
            { "SOC", ETypeFile.Image },
            { "USER", ETypeFile.Image },
            { "CUSTOMER", ETypeFile.Image},
            { "BLOG", ETypeFile.Image },
        };

        public static Dictionary<ETypeFile, string> TypeAccept = new()
        {
            { ETypeFile.Image, ".jpg, .jpeg, .png, .icon" },
            { ETypeFile.File, ".jpg, .jpeg, .png, .icon, .doc, .docx, .xls, .xlsx, .pdf, .pptx, .ppt, .txt" },
        };

        public static bool CheckTypeAccept(string typeFile, string ext)
        {
            var eTypeFile = TypeFile[typeFile];
            var typeAccept = TypeAccept[eTypeFile];
            if (typeAccept.Contains(ext.ToLower()))
            {
                return true;
            }
            return false;
        }

        public enum ETypeSort
        {
            NULL,
            AZ,
            ZA,
            PRICELOW,
            PRICEHIGH
        }
    }
}
