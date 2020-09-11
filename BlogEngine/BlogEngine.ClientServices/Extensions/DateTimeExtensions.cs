using System;

namespace BlogEngine.ClientServices.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetRelativeTime(this DateTime dateTime /*bool firstLetterUppercase = false*/)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);

            string resultString;

            if (timeSpan.Days > 0)
                resultString = $"{timeSpan.Days} Days";

            else if (timeSpan.Hours > 0)
                resultString = $"{timeSpan.Hours} Hours";

            else if (timeSpan.Minutes > 0)
                resultString = $"{timeSpan.Minutes} Minutes";

            else if (timeSpan.Seconds > 0)
                resultString = $"{timeSpan.Seconds} Seconds";
            else
                return null;

            /*
            if (firstLetterUppercase)
            {
                resultString = resultString.ToUpper();
            }
            */

            return resultString;
        }
    }
}