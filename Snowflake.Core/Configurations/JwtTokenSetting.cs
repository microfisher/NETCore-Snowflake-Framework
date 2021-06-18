using System;
namespace Snowflake.Core.Configurations
{
    /// <summary>
    /// JwtToken配置
    /// </summary>
    public class JwtTokenSetting
    {
        public int Expired { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
}
