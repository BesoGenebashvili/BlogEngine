using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using BlogEngine.Shared.Helpers;

namespace BlogEngine.ClientServices.Common.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            Preconditions.NotNull(enumValue, nameof(enumValue));

            var enumMember = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            Preconditions.NotNull(enumMember, nameof(enumMember));

            return enumMember.GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return GetAttribute<DisplayAttribute>(enumValue)?.Name;
        }
    }
}