using BlogEngine.Shared.Helpers;
using System;
using System.Linq;
using System.Web;

namespace BlogEngine.ClientServices.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToQueryString(this object obj)
        {
            Preconditions.NotNull(obj, nameof(obj));

            var properties = obj.GetType().GetProperties()
                .Where(p => p.GetValue(obj, null) != null)
                .Select(p => p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString()))
                .ToArray();

            return string.Join("&", properties);
        }
    }
}