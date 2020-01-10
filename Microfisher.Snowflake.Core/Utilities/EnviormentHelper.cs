using System;
using System.Net;
using System.Net.Sockets;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class EnviormentHelper
    {
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            var ip = string.Empty;
            var list = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (var address in list)
            {
                if (address.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    continue;
                }
                ip = address.ToString();
                break;
            }

            if (string.IsNullOrEmpty(ip))
            {
                ip = list[0].ToString();
            }
            return ip;
        }
    }
}
