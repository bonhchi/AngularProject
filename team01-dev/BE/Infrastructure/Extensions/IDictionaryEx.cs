using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class IDictionaryEx
    {
        public static string GetVal(this IDictionary<Guid, string> dic, Guid key)
        {
            dic.TryGetValue(key, out string val);
            return val ?? string.Empty;
        }

        public static Guid GetVal(this IDictionary<string, Guid> dic, string key)
        {
            if (key.IsValid())
            {
                dic.TryGetValue(key, out Guid val);
                return val;
            }
            return Guid.Empty;
        }

        public static string GetVal(this IDictionary<string, string> dic, string key)
        {
            if (key.IsValid())
            {
                dic.TryGetValue(key, out string val);
                return val;
            }
            return string.Empty;
        }

        public static IList<string> GetVals(this IDictionary<Guid, string> dic, IList<Guid> keys)
        {
            IList<string> result = new List<string>();
            if (keys?.Any() == true)
            {
                result = dic.Where(x => keys.Contains(x.Key)).Select(x => x.Value).ToList();
            }
            return result;
        }
    }
}
