using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlogEngine.Server.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            finally
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"Request: {httpContext.Request?.Method}, ");
                stringBuilder.Append($"URL: {httpContext.Request?.Path.Value}, ");
                stringBuilder.Append($"StatusCode: {httpContext.Response?.StatusCode}, ");
                stringBuilder.Append($"Time: {DateTime.Now}.");

                _logger.LogInformation(stringBuilder.ToString());
            }
        }
    }
}