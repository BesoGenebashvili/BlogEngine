using BlogEngine.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace BlogEngine.Server.Attributes
{
    public class HATEOASAttribute : ResultFilterAttribute
    {
        protected bool ShouldIncludeHATEOAS(ResultExecutingContext resultExecutingContext)
        {
            var objectResult = resultExecutingContext.Result as ObjectResult;

            if (!objectResult.IsSuccessfulResponse())
            {
                return false;
            }

            var header = resultExecutingContext.HttpContext.Request.Headers["IncludeHATEOAS"];

            bool.TryParse(header.FirstOrDefault(), out bool includeHATEOAS);

            return includeHATEOAS;
        }
    }
}