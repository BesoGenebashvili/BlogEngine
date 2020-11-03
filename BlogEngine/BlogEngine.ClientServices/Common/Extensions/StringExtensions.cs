using System;

namespace BlogEngine.ClientServices.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetStringBrief(this string s, int count = 20)
        {
            if (!string.IsNullOrEmpty(s) && s.Length > count)
            {
                try
                {
                    return s.Substring(0, count - 3) + "...";
                }
                catch (Exception)
                {
                    return s;
                }
            }
            else
            {
                return s;
            }
        }
    }
}