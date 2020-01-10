using System;
using System.ComponentModel.DataAnnotations;
using Microfisher.Snowflake.Core.Utilities;

namespace Microfisher.Snowflake.Services.Dtos
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
