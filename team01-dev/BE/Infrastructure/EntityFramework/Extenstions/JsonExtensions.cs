using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Infrastructure.EntityFramework.Extensions
{
    public static class JsonExtensions
    {
        public static string JsonValue(string column, [NotParameterized] string path)
        {
            throw new NotSupportedException();
        }

        public static int OpenJson(string column, [NotParameterized] string path)
        {
            throw new NotSupportedException();
        }
        public static int OpenJsonValue(string column, [NotParameterized] string path, [NotParameterized] string value)
        {
            throw new NotSupportedException();
        }
        public static string NoneUnicode(string column)
        {
            throw new NotSupportedException();
        }
    }
}
