using BlogEngine.Server.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BlogEngine.Server.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}