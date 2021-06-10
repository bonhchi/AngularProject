using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.MD5
{
    public class MD5Helper
    {
        public static string ToMD5Hash(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return ToMD5Hash(Encoding.UTF8.GetBytes(str));
        }

        public static string ToMD5Hash(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                return string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
            }
        }
    }
}
