using System;
using System.Collections.Generic;

namespace Snowflake.Core.Configurations
{
    /// <summary>
    /// 系统应用配置
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 应用名称配置
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 允许跨域访问地址配置
        /// </summary>
        public List<string> CORS { get; set; }

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisSetting Redis { get; set; }

        /// <summary>
        /// JwtToken配置
        /// </summary>
        public JwtTokenSetting JwtToken { get; set; }

        /// <summary>
        /// 消息队列配置
        /// </summary>
        public RabbitmqSetting RabbitMQ { get; set; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        public DatabaseSetting Database { get; set; }

    }
}