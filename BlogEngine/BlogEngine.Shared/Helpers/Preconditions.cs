using System;

namespace BlogEngine.Shared.Helpers
{
    public static class Preconditions
    {
        public static object NotNull(object obj, string paramName)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }

        public static string NotNullOrWhiteSpace(string text, string paramName)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(paramName);
            }

            return text;
        }
    }
}