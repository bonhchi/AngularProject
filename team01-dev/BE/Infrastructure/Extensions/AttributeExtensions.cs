using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

        public static IList<TAttribute> GetAttributeType<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetProperties()
                .Select(prop => GetAttributeProperty<TAttribute>(prop))
                .Where(prop => prop != null)
                .ToList();
        }

        public static TAttribute GetAttributeProperty<TAttribute>(this PropertyInfo prop) where TAttribute : Attribute
        {
            return prop.GetCustomAttributes(
                typeof(TAttribute), false
                ).FirstOrDefault() as TAttribute;
        }
    }
}
