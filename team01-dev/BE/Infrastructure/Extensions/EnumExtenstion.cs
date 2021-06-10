using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class EnumExtenstion
    {
        public static string GetName(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }
        //public static string ToJson(this Enum enu)
        //{
        //    var name = enu.GetName();
        //    var id = enu.GetHashCode();
        //    return Serializer.JsonHelpers.SerializeJson(new { Id = id, Name = name });
        //}

        public static string GetDescription(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Description : enu.ToString();
        }


        public static Attr GetAttribute<Attr>(object value) where Attr : Attribute
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }
            var field = type.GetField(value.ToString());
            return field == null ? default : field.GetCustomAttribute<Attr>();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }

        public static string GetDisplayOrNameAttribute<T>(this T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes != null && descriptionAttributes.Any() && descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        /// <summary>
        ///  Get Display from Resource
        /// </summary>        
        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            if (string.IsNullOrEmpty(resourceKey)) return string.Empty;
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }
    }
}
