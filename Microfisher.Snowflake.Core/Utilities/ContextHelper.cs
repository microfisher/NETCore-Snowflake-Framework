using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class ContextHelper
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP(IHttpContextAccessor httpContextAccessor)
        {
            var ip = httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return string.Empty;
        }


        /// <summary>
        /// 获取当前网址
        /// </summary>
        public static string GetHostUrl(IHttpContextAccessor httpContextAccessor, bool isWithScheme = false, bool isWithPort = false)
        {
            var scheme = httpContextAccessor.HttpContext.Request.Scheme;
            var host = httpContextAccessor.HttpContext.Request.Host.Host;
            var port = httpContextAccessor.HttpContext.Request.Host.Port;
            return $"{(isWithScheme ? $"{scheme}://" : "")}{host}{(isWithPort ? $"{port}" : "")}";
        }
    }
}
