using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class ValidateHelper
    {

        /// <summary>
        /// 验证数字
        /// </summary>
        public static bool IsNumeric(string value)
        {
            return value.All(Char.IsDigit);
        }

        /// <summary>
        /// 验证帐号（帐号只能是英文字符及数字组合）
        /// </summary>
        public static bool IsUserName(string account)
        {
            return Regex.IsMatch(account, @"^[A-Za-z0-9.]{1,16}$");
        }

        /// <summary>
        /// 邀请码
        /// </summary>
        public static bool IsReferrer(string number)
        {
            return Regex.IsMatch(number, @"^[A-Za-z0-9]{6,20}$");
        }

        /// <summary>
        /// 交易密码
        /// </summary>
        public static bool IsPaySecret(string password)
        {
            return Regex.IsMatch(password, @"(?=.*\d)(?=.*[a-zA-Z]).{6,16}");
        }

        /// <summary>
        /// 登录密码（一定要字母和数字组合）
        /// </summary>
        public static bool IsPassword(string password)
        {
            return Regex.IsMatch(password, @"(?=.*\d)(?=.*[a-zA-Z]).{6,16}") && Regex.IsMatch(password, @"^[a-zA-Z0-9]\w{5,17}$");
        }

        /// <summary>
        /// 验证昵称（昵称只能为汉字、数字、字符、下划线）
        /// </summary>
        public static bool IsNickName(string nickname)
        {
            return Regex.IsMatch(nickname, @"^([\u4E00-\u9FA5A-Za-z0-9_-]){0,16}$");
        }

        /// <summary>
        /// 验证ID（ID只能为、字母、数字、下划线）
        /// </summary>
        public static bool IsIDNumber(string number)
        {
            return Regex.IsMatch(number, @"^([A-Za-z0-9_-]){0,32}$");
        }

        /// <summary>
        /// 验证邮箱（符合邮箱一般规则）
        /// </summary>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-z,A-Z,0-9]{1,10}((-|.)\w+)*@\w+.\w{2,16}$");
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        public static bool IsPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^[0-9+]{0,20}$");
        }

        /// <summary>
        /// 验证弱密码
        /// </summary>
        public static bool IsWeakPassword(string loginPwd)
        {
            string weakPwdList = "a12345,ab1234,abc123,a1b2c3,aaa111,123qwe";
            return weakPwdList.Split(',').ToList().Contains(loginPwd.Trim().ToLower());
        }

        /// <summary>
        /// 验证IPV4
        /// </summary>
        public static bool IsIPv4(string ipAddress)
        {
            string[] IPs = ipAddress.Split('.');
            if (IPs.Length != 4)
                return false;
            int n = -1;
            for (int i = 0; i < IPs.Length; i++)
            {
                if (i == 0 || i == 3)
                {
                    if (int.TryParse(IPs[i], out n) && n > 0 && n < 255)
                        continue;
                    else
                        return false;
                }
                else
                {
                    if (int.TryParse(IPs[i], out n) && n >= 0 && n <= 255)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }

        /// <summary>  
        /// 验证IPv6地址  
        /// </summary>  
        public static bool IsIPv6(string ipAddress)
        {
            string pattern = @"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$";
            return Regex.IsMatch(ipAddress, pattern);
        }



    }
}
