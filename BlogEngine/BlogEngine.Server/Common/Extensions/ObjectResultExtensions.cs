using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Server.Common.Extensions
{
    public static class ObjectResultExtensions
    {
        public static bool IsSuccessfulResponse(this ObjectResult objectResult)
        {
            if (objectResult is null || objectResult.Value is null)
            {
                return false;
            }

            if (objectResult.StatusCode.HasValue && !objectResult.StatusCode.Value.ToString().StartsWith("2"))
            {
                return false;
            }

            return true;
        }
    }
}