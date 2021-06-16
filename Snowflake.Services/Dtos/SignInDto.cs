using System;
using System.ComponentModel.DataAnnotations;

namespace Snowflake.Services.Dtos
{
    public class SignInDto
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
