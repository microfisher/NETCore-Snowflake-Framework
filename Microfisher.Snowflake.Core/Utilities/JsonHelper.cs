using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class JsonHelper
    {
        /// <summary>
        /// 反序列化对象
        /// </summary>
        public static T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { IgnoreNullValues = true, IgnoreReadOnlyProperties = false });
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        public static string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize<T>(data, new JsonSerializerOptions { IgnoreNullValues = true, IgnoreReadOnlyProperties = false });
        }
    }
}
