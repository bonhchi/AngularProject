using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class ValidatationExtensions
    {
        public static string UnixTimeStampUtc()
        {
            var g = Guid.NewGuid();
            return g.ToString();
        }

        public static bool IsNullOrEmpty(this object obj)
        {
            return obj == null;
        }
        public static bool IsNotNullOrEmpty(this object obj)
        {
            return obj != null;
        }
        public static bool IsNotNullOrGuidDefault(this Guid obj)
        {
            return obj != null && obj == Guid.Empty;
        }
        public static bool IsNotNullOrGuidDefault(this Guid? obj)
        {
            return obj != null && obj == Guid.Empty;
        }
        public static bool IsNotNullOrEmpty(this IList array)
        {
            return array != null && array.Count > 0;
        }
        public static bool IsNullOrEmpty(this string obj)
        {
            return obj == null || obj.Length < 0;
        }
        public static bool IsNullOrEmpty(this Guid? obj)
        {
            return obj == null || obj == Guid.Empty;
        }
        public static bool IsNotNullOrEmpty(this string obj)
        {
            return !string.IsNullOrWhiteSpace(obj);
        }
        public static string GetTime(this string obj)
        {
            if (!string.IsNullOrWhiteSpace(obj))
            {
                var objs = obj.Split(':');
                if (objs.IsNotNullOrEmpty())
                {
                    var hours = objs[0];
                    var mi = objs[1];
                    if (hours.Length == 1)
                    {
                        hours = $"0{hours}";
                    }
                    if (mi.Length == 1)
                    {
                        mi = $"{mi}0";
                    }
                    obj = $"{hours}:{mi}";
                }
            }
            return obj;
        }
        public static int GetHour(this string obj)
        {
            var result = 0;
            if (!string.IsNullOrWhiteSpace(obj))
            {
                var objs = obj.Split(':');
                if (objs.IsNotNullOrEmpty())
                {
                    var hours = objs[0];
                    result = Convert.ToInt32(hours);
                }
            }
            return result;
        }
        public static int GetMinute(this string obj)
        {
            var result = 0;
            if (!string.IsNullOrWhiteSpace(obj))
            {
                var objs = obj.Split(':');
                if (objs.IsNotNullOrEmpty())
                {
                    var minutes = objs[1];
                    result = Convert.ToInt32(minutes);
                }
            }
            return result;
        }
        public static bool IsList(this object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(this object o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }
        public static bool IsNullOrEmpty(this IEnumerable<object> obj)
        {
            return obj == null || !obj.Any();
        }
        public static bool IsNotNull(this Guid obj)
        {
            return obj != null && obj != Guid.Empty;
        }
        public static bool IsNotNull(this Guid? obj)
        {
            return obj != null && obj != Guid.Empty;
        }

        public static bool CompareGuid(this Guid? mainId, Guid? compareId)
        {
            if (!mainId.IsNotNull() && !compareId.IsNotNull()) return true;
            if (mainId.IsNotNull() && !compareId.IsNotNull()) return false;
            if (!mainId.IsNotNull() && compareId.IsNotNull()) return false;
            return mainId.Value.CompareTo(compareId.Value) == 0;
        }

        public static int Replace<T>(this IList<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var index = source.IndexOf(oldValue);
            if (index != -1)
                source[index] = newValue;
            return index;
        }


        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.Select(x => EqualityComparer<T>.Default.Equals(x, oldValue) ? newValue : x);
        }

        public static TR CopyToOtherObject<T, TR>(this T instance, TR cell2)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var value = propertyInfos[i].GetValue(instance);
                cell2.GetType().GetProperties()[i].SetValue(cell2, value);
            }
            return cell2;
        }

        public static T CopyTo<T>(this T instance, T cell2)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties().OrderBy(x => x.Name).ToArray();
            PropertyInfo[] propertyInfos2 = cell2.GetType().GetProperties().OrderBy(x => x.Name).ToArray();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var value = propertyInfos[i].GetValue(instance);
                propertyInfos2[i].SetValue(cell2, value);
            }
            return cell2;
        }

        public static T CopyAll<T>(this T instance) where T : new()
        {
            T cell2 = new T();
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties().OrderBy(x => x.Name).ToArray();
            PropertyInfo[] propertyInfos2 = cell2.GetType().GetProperties().OrderBy(x => x.Name).ToArray();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var value = propertyInfos[i].GetValue(instance);
                if (!value.IsNullOrEmpty() && value.IsList())
                {
                    List<T> lstData = new List<T>();
                    foreach (var item in (List<T>)value)
                    {
                        T newItem;
                        newItem = item.CopyAll();
                        lstData.Add(newItem);
                    }
                    propertyInfos2[i].SetValue(cell2, lstData);
                }
                else
                {
                    propertyInfos2[i].SetValue(cell2, value);
                }
            }
            return cell2;
        }

        public static List<T> CopyList<T>(this IEnumerable<T> lstdata) where T : new()
        {
            List<T> lstResult = new List<T>();
            foreach (var item in lstdata)
            {
                var obj = item.CopyAll();
                lstResult.Add(obj);
            }
            return lstResult;
        }

        public static IEnumerable<T> TpTraverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> fnRecurse)
        {
            foreach (T item in source)
            {
                yield return item;

                IEnumerable<T> seqRecurse = fnRecurse(item);
                if (seqRecurse != null)
                {
                    foreach (T itemRecurse in TpTraverse(seqRecurse, fnRecurse))
                    {
                        yield return itemRecurse;
                    }
                }
            }
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static bool DateInWorkingDate(this DateTime date, int startDay, int endDay)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(date);
            var dateCompare = date.AddHours(offset.Hours);
            return (int)dateCompare.DayOfWeek >= startDay - 1 && (int)dateCompare.DayOfWeek <= endDay - 1;
        }
        public static bool DateInWorkingTime(this DateTime date, string startTime, string endTime)
        {
            bool result = false;
            if (startTime.IsNullOrEmpty() || endTime.IsNullOrEmpty())
                return false;
            string[] start = startTime.Split(":");
            string[] end = endTime.Split(":");

            try
            {
                int hour = date.Hour;
                int minute = date.Minute;
                int hourStart = int.Parse(start[0]);
                //int minuteStart = int.Parse(start[1]);
                int hourEnd = int.Parse(end[0]);
                //int minuteEnd = int.Parse(end[1]);

                if (hour > hourStart && hour < hourEnd)
                {
                    result = true;
                }
                //else if (hour == hourStart)
                //{
                //    result = minute >= minuteStart;
                //} else if (hour == hourEnd)
                //{
                //    result = minute <= minuteEnd;
                //}
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
    }
}
