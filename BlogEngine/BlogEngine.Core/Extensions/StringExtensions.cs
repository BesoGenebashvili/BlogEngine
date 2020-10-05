﻿using BlogEngine.Core.Helpers;
using System.Text.RegularExpressions;

namespace BlogEngine.Core.Extensions
{
    public static class StringExtensions
    {
        public static string StripHtmlTagsWithRegex(this string rawHtmlContent)
        {
            if (string.IsNullOrWhiteSpace(rawHtmlContent))
            {
                Throw.ArgumentNullException(nameof(rawHtmlContent));
            }

            Regex regex = new Regex("\\<[^\\>]*\\>");
            return regex.Replace(rawHtmlContent, string.Empty);
        }

        public static string StripHtmlTagsWithCharArray(this string rawHtmlContent)
        {
            if (string.IsNullOrWhiteSpace(rawHtmlContent))
            {
                Throw.ArgumentNullException(nameof(rawHtmlContent));
            }

            char[] array = new char[rawHtmlContent.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < rawHtmlContent.Length; i++)
            {
                char let = rawHtmlContent[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}