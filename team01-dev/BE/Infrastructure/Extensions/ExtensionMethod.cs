using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Infrastructure.Extensions
{
    public static class ExtensionMethod
    {
        public static ValidateModel Validate<TValue>(this TValue obj)
        {
            var validationResultList = new List<ValidationResult>();
            var result = Validator.TryValidateObject(obj, new ValidationContext(obj), validationResultList);
            return new ValidateModel { IsValid = result, Errors = validationResultList };
        }
        public static List<ValidationResult> ValidateModel<TValue>(this TValue obj)
        {
            var validationResultList = new List<ValidationResult>();
            Validator.TryValidateObject(obj, new ValidationContext(obj), validationResultList);
            return validationResultList;
        }

       

        public static DateTime StartOfDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public static DateTime EndOfDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

      
        public static Dictionary<string, TValue> ToDictionary<TValue>(this object obj)
        {
            if (obj != null)
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
                return dictionary;
            }
            return new Dictionary<string, TValue>();
        }

        public static T ToObject<T>(this IDictionary<string, string> source)
        {
            var json = JsonConvert.SerializeObject(source, Formatting.Indented);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        public static int GetIntValue(this object value, int dfault = 0)
        {
            try
            {
                if (value != null)
                    return int.Parse(value.ToString());
            }
            catch
            {

            }
            return dfault;
        }
        public static decimal GetDecimalValue(this object value, decimal dfault = 0)
        {
            try
            {
                if (value != null)
                    return decimal.Parse(value.ToString());
            }
            catch
            {

            }
            return dfault;
        }
        public static string GetStringValue(this object value)
        {
            try
            {
                return value.ToString();
            }
            catch
            {

            }
            return string.Empty;
        }


        public static double GetDoubleValue(this object value, double dfault = 0)
        {
            try
            {
                if (value != null)
                    return double.Parse(value.ToString());
            }
            catch
            {

            }
            return dfault;
        }
        public static string GetStringFromDatetime(this object value, string format = "dd/mm/YY", string dfault = "")
        {
            try
            {
                if (value != null)
                    return DateTime.Parse(value.ToString()).ToString(format);
            }
            catch
            {

            }
            return dfault;
        }
        public static DateTime ConvertStringTimeToDateTime(this string value)
        {
            try
            {
                var provider = new CultureInfo("en");
                var date = $"{DateTime.Now.ToString("dd/MM/yyyy")} {value.GetTime()}";
                if (value != null)
                    return DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", provider);
            }
            catch
            {

            }
            return DateTime.Now;
        }
        public static DateTime ConvertStringTimeToDateTime(this string value, DateTime data)
        {
            try
            {
                var provider = Thread.CurrentThread.CurrentCulture;
                var date = $"{data.ToString("dd/MM/yyyy")} {value.GetTime()}";
                if (value != null)
                    return DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", provider);
            }
            catch
            {

            }
            return DateTime.Now;
        }

        public static string GetImageNotFound(this object value, string path)
        {
            if (value == null)
            {
                return "~/assets/img/No_image_available.jpg";
            }

            return path + "/" + value.ToString();
        }
    }

    public class ValidateModel
    {
        public bool IsValid { get; set; }
        public List<ValidationResult> Errors { get; set; }
    }
}
