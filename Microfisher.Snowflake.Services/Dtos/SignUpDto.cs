using System;
namespace Microfisher.Snowflake.Services.Dtos
{
    public class SignUpDto
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
    }
}
