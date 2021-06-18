using System;
namespace Snowflake.Core.Configurations
{
    /// <summary>
    /// 消息队列配置
    /// </summary>
    public class RabbitmqSetting
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
