using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace Infrastructure.EntityFramework
{
    public static class DbFunctions
    {
        [DbFunction("JSON_VALUE", "")]
        public static string JsonValue(string column, [NotParameterized] string path)
        {
            throw new NotSupportedException();
        }
    }
}
