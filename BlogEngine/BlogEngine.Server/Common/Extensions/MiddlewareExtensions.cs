﻿using BlogEngine.Server.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BlogEngine.Server.Common.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingMiddleware>();
        }
    }
}