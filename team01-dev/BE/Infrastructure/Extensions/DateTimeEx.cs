using System;

namespace Infrastructure.Extensions
{
    public static class DateTimeEx
    {
        const string VNTimeZoneKey = "SE Asia Standard Time";

        public static DateTime? IsMinDate(this DateTime d, bool toUtc = true)
        {
            if (d.Date.Equals(DateTime.MinValue.Date))
                return null;
            else
                return toUtc ? d.ToUniversalTime() : d;
        }

        public static bool IsValid(this DateTime? d)
        {
            return d.HasValue && d > DateTime.MinValue;
        }

        public static bool IsValid(this DateTime d)
        {
            return d > DateTime.MinValue;
        }

        public static string DateTimeFormat(this DateTime d, string format)
        {
            return string.Format(format, d);
        }

        public static DateTime EndOfDay(this DateTime d)
        {
            return d.Date.AddDays(1).AddTicks(-1);
        }

        public static DateTime RemoveMinute(this DateTime d)
        {
            return d.Date.AddHours(d.Hour);
        }

        public static DateTime RemoveSecond(this DateTime d)
        {
            return d.RemoveMinute().AddMinutes(d.Minute);
        }

        public static DateTime UTC0To(this DateTime d, string timezone = VNTimeZoneKey)
        {
            var tzone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return TimeZoneInfo.ConvertTimeFromUtc(d, tzone);
        }

        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)timeSpan.TotalMilliseconds;
        }

      
    }
}