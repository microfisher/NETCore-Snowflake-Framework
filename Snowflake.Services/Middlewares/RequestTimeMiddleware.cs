using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Snowflake.Services.Middlewares
{
    public class RequestTimeMiddleware
    {
        private readonly ILogger _logger;

        private readonly RequestDelegate _next;

        public RequestTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestTimeMiddleware>();
        }

        public Task InvokeAsync(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            context.Response.OnStarting(() =>
            {
                stopWatch.Stop();
                var requestTime = stopWatch.ElapsedMilliseconds;
                context.Response.Headers["X-Request-Time"] = requestTime.ToString();

                if (requestTime >= 1000)
                {
                    _logger.LogWarning($"系统接口：{context.Request.Path}耗时({requestTime}ms)");
                }

                return Task.CompletedTask;
            });

            return _next(context);
        }
    }
}
