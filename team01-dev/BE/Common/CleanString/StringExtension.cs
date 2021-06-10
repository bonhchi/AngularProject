using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.StringEx
{
    public class StringExtension
    {
        public static string CleanString(string model)
        {
            if (model.Trim() == "")
            {
                return null;
            }
            string currentItem = "";
            currentItem = model.Trim();
            currentItem = Regex.Replace(currentItem, @"\s+", " ");

            return currentItem;
        }
    }
}
