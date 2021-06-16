using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Snowflake.Services.Middlewares
{
    public class IPAddressMiddleware
    {
        private readonly RequestDelegate _next;

        public IPAddressMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;
            if (headers.ContainsKey("X-Forwarded-For"))
            {
                context.Connection.RemoteIpAddress = IPAddress.Parse(headers["X-Forwarded-For"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)[0]);
            }
            return _next(context);
        }
    }
}
