
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidJson(this string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }


        public static string GetInitial(this string names, string separator = "")
        {
            var extractInitials = new Regex(@"\s*([^\s])[^\s]*\s*");
            return extractInitials.Replace(names, "$1" + separator).ToUpper();
        }
        public static string ReplaceTemplate(this string text, object data)
        {
            try
            {
                var propertyInfos = data.GetType().GetProperties();
                var templateData = propertyInfos.ToDictionary(x => $"{{{x.Name}}}", y => y.GetValue(data).ToStringWithFormat());
                return text.MultipleReplace(templateData);
            }
            catch
            {
                return text;
            }
        }
        public static string MultipleReplace(this string text, Dictionary<string, string> replacements)
        {
            try
            {
                foreach (string textToReplace in replacements.Keys)
                {
                    text = text.Replace(textToReplace, replacements[textToReplace]);
                }

                return text;
            }
            catch
            {
                return text;
            }
        }
        public static string ToStringWithFormat(this object val)
        {
            if (val != null)
                return IsNumber(val) ? $"{val:#,##}" : IsDateTime(val) ? $"{val:dd/MM/yyyy}" : val.ToString();
            return string.Empty;
        }
        public static bool IsValid(this string val)
        {
            return !string.IsNullOrWhiteSpace(val);
        }

        public static string ToJson(this object obj)
        {
            if (obj == null) return string.Empty;
            return JsonConvert.SerializeObject(obj);
        }

        public static Guid ToGuid(this string val)
        {
            var result = Guid.Empty;
            return val.IsValid() && (val.Length == 32 || val.Length == 36) && Guid.TryParse(val, out result) ? result : Guid.Empty;
        }

        public static int ToInt(this string val)
        {
            int result = 0;
            int.TryParse(val, out result);
            return result;
        }

        public static decimal ToDecimal(this string val)
        {
            decimal result = 0;
            decimal.TryParse(val, out result);
            return result;
        }

        public static DateTime ToDate(this string value, string format)
        {
            var result = DateTime.MinValue;
            if (value.IsValid())
                DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out result);
            return result;
        }

        public static bool ToBool(this string value)
        {
            bool result = value.IsValid() && value.Equals("x", StringComparison.CurrentCultureIgnoreCase);
            if (!result && value.IsValid())
                bool.TryParse(value, out result);
            return result;
        }

        public static string EmptyIfNull(this object value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }

        public static string ToBase64(this string val)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(val));
        }

        public static string ToBase64<T>(this T obj)
        {
            return obj?.ToJson()?.ToBase64();
        }

        public static T FromBase64<T>(this string val, bool gzip = false)
        {
            if (val.IsValid())
            {
                var bytes = Convert.FromBase64String(val);
                byte[] outputBytes = null;
                if (gzip)
                {
                    using (var zippedStream = new MemoryStream(bytes))
                    {
                        using (var archive = new ZipArchive(zippedStream))
                        {
                            var entry = archive.Entries.FirstOrDefault();
                            if (entry != null)
                            {
                                using (var unzippedEntryStream = entry.Open())
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        unzippedEntryStream.CopyTo(ms);
                                        outputBytes = ms.ToArray();
                                    }
                                }
                            }
                        }
                    }
                }

                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(outputBytes ?? bytes));
            }

            return default(T);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string Md5Hash(this string str)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(str));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        public static string ToStringIds(this string[] ids, string splitChar = ",")
        {
            return ids?.Any() == true ? string.Join(splitChar, ids) : string.Empty;
        }

        public static bool IsEmailValid(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static string ToUnSignNotChar(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }

            input = input.Trim();

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }

            return str2.Trim();
        }

        public static string ToUnSign(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), "");
            }

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }

            return str2.ToLower().Trim();
        }
        public static bool CheckSearch(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            input = input.Trim();
            var index = input.IndexOf("\"");
            var lastIndex = input.LastIndexOf("\"");
            if (index != lastIndex && index != -1 && lastIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string ToUnSign(this string input, bool isCLean = true)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            input = input.Trim().Replace("%", "");
            var index = input.IndexOf("\"");
            var lastIndex = input.LastIndexOf("\"");
            if (index != lastIndex && index != -1 && lastIndex != -1 && (index < lastIndex) && isCLean)
            {
                var data = input.Substring(index, (lastIndex - index));
                data = data.Replace("\"", "");
                return data.ToLower();
            }
            else
            {
                if (isCLean == false)
                {
                    input = input.Replace("'", "").Replace("\"", "");
                    Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
                    string str = input.Normalize(NormalizationForm.FormD);
                    string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
                    while (str2.IndexOf("?") >= 0)
                    {
                        str2 = str2.Remove(str2.IndexOf("?"), 1);
                    }

                    return str2.ToLower().Trim();
                }
                return input;
            }
        }



        public static bool IsValidFileName(this string expression, bool platformIndependent = true)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern =
                    @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\.~!@#$%^&*+= ](([^\\/:\*\?""\|<>\.~!@#$%^&*+= ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\.~!@#$%^&*+= ]))?))\\)*[^\\/:\*\?""\|<>\.~!@#$%^&*+= ](([^\\/:\*\?""\|<>\.~!@#$%^&*+= ])|([^\\/:\*\?""\|<>~!@#$%^&*+=]*[^\\/:\*\?""\|<>\.~!@#$%^&*+= ]))?$";
            }

            return (Regex.IsMatch(expression, sPattern, RegexOptions.CultureInvariant));
        }

        public static string ToReplaceFileName(this string fileName)
        {
            var fileCharsInit = new List<char>() { '@', '~', '!', '#', '$', '%', '^', '&', '(', ')', '+', '=', '?', '<', '>', '{', '}', '[', ']', '|', '`', ':', ';', '"', '\'', '?', ',', '/', '\\' };
            fileName = fileName.Trim();
            fileName = fileName.Replace(" ", "-");
            foreach (var item in fileCharsInit)
            {
                fileName = fileName.Replace(item.ToString(), "");
            }

            return fileName.ToUnSignNotChar();
        }

        public static bool IsNumber(this object val)
        {
            return val is sbyte
                   || val is byte
                   || val is short
                   || val is ushort
                   || val is int
                   || val is uint
                   || val is long
                   || val is ulong
                   || val is float
                   || val is double
                   || val is decimal;
        }

        public static bool IsDateTime(this object val)
        {
            return val is DateTime;
        }


        public static string ReformatEmails(this string emails)
        {
            if (!emails.IsValid())
                return string.Empty;

            var validEmails = emails.Split(new[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.IsEmailValid()).Select(x => x.Trim());
            return string.Join(";", validEmails);
        }

        public static DateTime FromUnixTimeStampToDateTime(this string unixTimeStamp, bool toUTC = false)
        {
            return toUTC ? DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimeStamp)).UtcDateTime :
                DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimeStamp)).DateTime;
        }

        public static DateTime ToDateTimeFromEpoch(this string epochTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 1, System.DateTimeKind.Utc);
            return unixStart.AddMilliseconds(long.Parse(epochTime));
        }

        public static string ValueIdx(this IList<string> arr, int index)
        {
            return arr.Count > index ? arr[index].Trim() : string.Empty;
        }

        public static string SanitizeJavascript(this string html)
        {
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            return html.IsNotNullOrEmpty() ? rRemScript.Replace(html, "") : html;
        }

        public static bool IsFemale(this string input)
        {

            if (!string.IsNullOrWhiteSpace(input) && (input.Trim().ToLower() == "female"))
            {
                return true;
            }
            return false;
        }
    }
}
