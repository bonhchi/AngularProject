using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Infrastructure.Utilities
{
    public class DateUtilities
    {

        public static int GetWeeksInYear(int year)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
        }

        public static int WorkDaysInMonth(DateTime startDate, DateTime endDate, int startDay, int endDay)
        {
            var startDateMonth = new DateTime(startDate.Year, startDate.Month, 1);
            var endDateMonth = new DateTime(endDate.Year, endDate.AddMonths(1).Month, 1).AddDays(-1);
            if (endDate.Year == DateTime.Today.Year && endDate.Month == DateTime.Today.Month)
            {
                endDateMonth = DateTime.Today;
            }

            List<DateTime> dates = new List<DateTime>();
            for (var i = startDateMonth; i.Date <= endDateMonth.Date; i = i.AddDays(1))
            {
                dates.Add(i);
            }
            int workDays = dates.Count(d => d.DayOfWeek.GetHashCode() >= startDay && d.DayOfWeek.GetHashCode() <= endDay);
            return workDays;
        }
        public static int WorkDaysInRange(DateTime startDate, DateTime endDate, int startDay, int endDay)
        {
            var startDateMonth = startDate;
            var endDateMonth = endDate;

            List<DateTime> dates = new List<DateTime>();
            for (var i = startDateMonth; i.Date <= endDateMonth.Date; i = i.AddDays(1))
            {
                dates.Add(i);
            }
            int workDays = dates.Count(d => d.DayOfWeek.GetHashCode() >= startDay - 1 && d.DayOfWeek.GetHashCode() <= endDay - 1);
            return workDays;
        }
        public static List<DateTime> ListDateWorkDaysInRange(DateTime startDate, DateTime endDate, int startDay, int endDay)
        {
            var startDateMonth = startDate;
            var endDateMonth = endDate;

            List<DateTime> dates = new List<DateTime>();
            for (var i = startDateMonth; i.Date <= endDateMonth.Date; i = i.AddDays(1))
            {
                dates.Add(i);
            }
            return dates.Where(d => d.DayOfWeek.GetHashCode() >= startDay - 1 && d.DayOfWeek.GetHashCode() <= endDay - 1).ToList();
        }
        public static List<DateTime> ListDateNonWorkDaysInRange(DateTime startDate, DateTime endDate, int startDay, int endDay)
        {
            var startDateMonth = startDate;
            var endDateMonth = endDate;

            List<DateTime> dates = new List<DateTime>();
            for (var i = startDateMonth; i.Date <= endDateMonth.Date; i = i.AddDays(1))
            {
                dates.Add(i);
            }
            return dates.Where(d => d.DayOfWeek.GetHashCode() < startDay - 1 || d.DayOfWeek.GetHashCode() > endDay - 1).ToList();
        }
        public static List<DateTime> ListDateWorkOnMonthDaysInRange(DateTime date, int startDay, int endDay)
        {
            var endDateMonth = date;
            var startDateMonth = date;
            if (date.Day == 1)
            {
                startDateMonth = date;
                endDateMonth = date.AddMonths(1).AddDays(-1);
            }
            else if (date.Day != 1)
            {
                startDateMonth = date.AddMonths(-1).AddDays(1);
                endDateMonth = date;
            }

            List<DateTime> dates = new List<DateTime>();
            for (var i = startDateMonth; i.Date <= endDateMonth.Date; i = i.AddDays(1))
            {
                dates.Add(i);
            }
            return dates.Where(d => d.DayOfWeek.GetHashCode() >= startDay - 1 && d.DayOfWeek.GetHashCode() <= endDay - 1).ToList();
        }

        public static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;

            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            var weekNumber = (date - firstMonthMonday).Days / 7 + 1;
            return weekNumber;
        }

        public static DateTime ConvertStringTimeToDateTime(string time)
        {
            var today = DateTime.Today;
            var times = time.Split(':');
            if (times.Length == 2)
            {
                var hours = Convert.ToInt32(times[0]);
                var minute = Convert.ToInt32(times[1]);
                return new DateTime(today.Year, today.Month, today.Day, hours, minute, 0);
            }
            return today;
        }
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }
    }
}
