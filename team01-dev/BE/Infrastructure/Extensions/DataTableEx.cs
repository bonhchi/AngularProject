using System;
using System.Data;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class DataTableEx
    {
        public static DataTable GetTable(this DataTableCollection tables, string tblName)
        {
            return tables != null && tables.Contains(tblName) ? tables[tblName] : null;
        }

        public static bool IsValidHeaders(this DataTable dt, string[] requiredHeaders)
        {
            var headerNames = dt.HeaderNames();
            return requiredHeaders.All(x => headerNames.Contains(x.ToLower()));
        }

        public static string[] HeaderNames(this DataTable dt)
        {
            return dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName.ToLower()).ToArray();
        }

        public static string StrVal(this DataRow rw, string colName)
        {
            return rw[colName.ToLower()]?.ToString()?.Trim() ?? string.Empty;
        }

        public static int IntVal(this DataRow rw, string colName)
        {
            return rw.StrVal(colName).ToInt();
        }

        public static DateTime? DateVal(this DataRow rw, string colName)
        {
            DateTime? val = rw.StrVal(colName).ToDate("MM/dd/yyyy");
            if (!val.IsValid())
                val = rw.StrVal(colName).ToDate("yyyy-MM-dd");
            if (!val.IsValid())
                val = rw.StrVal(colName).ToDate("MM/dd/yyyy HH:mm:ss");
            if (!val.IsValid())
                val = rw.StrVal(colName).ToDate("yyyy-MM-dd HH:mm:ss");
            return val.IsValid() ? val : null;
        }
    }
}