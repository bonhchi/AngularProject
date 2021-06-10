using System.Linq;

namespace Infrastructure.Extensions
{
    public static class IntEx
    {
        public static string ToStringIds(this int[] ids)
        {
            return ids?.Any() == true ? string.Join(",", ids) : string.Empty;
        }


    }
}