using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BlogEngine.ClientServices.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException(nameof(enumValue));
            }

            var enumMember = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (enumMember == null)
            {
                throw new ArgumentNullException(nameof(enumMember));
            }

            return enumMember.GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return GetAttribute<DisplayAttribute>(enumValue).Name;
        }
    }
}