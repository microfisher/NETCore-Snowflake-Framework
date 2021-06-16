using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Snowflake.Core.Utilities
{
    public static class TwoFactorHelper
    {
        /// <summary>
        /// 生成安装二维码Url
        /// </summary>
        /// <param name="issuer">网站域名</param>
        /// <param name="account">用户账号</param>
        /// <param name="secretKey">用户密钥</param>
        public static string GetFactorUrl(string issuer, string account, string secretKey)
        {
            var authUrl = $"otpauth://totp/{account}?secret={secretKey}&issuer={issuer}";
            return authUrl;
        }

        ///// <summary>
        ///// 生成安装二维码图片
        ///// </summary>
        ///// <param name="issuer">网站域名</param>
        ///// <param name="account">用户账号</param>
        ///// <param name="secretKey">用户密钥</param>
        //public static string GetFactorImage(string issuer, string account, string secretKey)
        //{
        //    var authUrl = $"otpauth://totp/{account}?secret={secretKey}&issuer={issuer}";
        //    var generator = new QRCodeGenerator();
        //    var codeData = generator.CreateQrCode(authUrl, QRCodeGenerator.ECCLevel.Q);
        //    var qrCode = new Base64QRCode(codeData);
        //    var base64 = qrCode.GetGraphic(2);
        //    return $"data:image/png;base64,{base64}";
        //}

        /// <summary>
        /// 验证双因素码
        /// </summary>
        /// <param name="code">用户app端生成的6位验证码</param>
        /// <param name="secretKey">网站保存的用户密钥</param>
        /// <returns></returns>
        public static bool ValidateCode(int code, string secretKey)
        {
            var codes = new List<int>();
            var iterationOffset = 0;
            var utcTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var iterationCounter = (long)(DateTime.UtcNow - utcTime).TotalSeconds / 30;
            var timeTolerance = TimeSpan.FromSeconds(60);
            if (timeTolerance.TotalSeconds > 30)
            {
                iterationOffset = Convert.ToInt32(timeTolerance.TotalSeconds / 30.00);
            }
            var iterationStart = iterationCounter - iterationOffset;
            var iterationEnd = iterationCounter + iterationOffset;
            for (var counter = iterationStart; counter <= iterationEnd; counter++)
            {
                var hash = ComputeHash(secretKey, counter, 6);
                codes.Add(hash);
            }
            return codes.ToArray().Any(c => c == code);
        }

        /// <summary>
        /// Google哈希
        /// </summary>
        public static int ComputeHash(string secret, long iterationNumber, int digits = 6)
        {
            var key = Encoding.UTF8.GetBytes(secret);

            var counter = BitConverter.GetBytes(iterationNumber);

            if (BitConverter.IsLittleEndian) Array.Reverse(counter);

            var hmac = new HMACSHA1(key);

            var hash = hmac.ComputeHash(counter);

            var offset = hash[hash.Length - 1] & 0xf;

            // Convert the 4 bytes into an integer, ignoring the sign.
            var binary =
                ((hash[offset] & 0x7f) << 24)
                | (hash[offset + 1] << 16)
                | (hash[offset + 2] << 8)
                | hash[offset + 3];

            var password = binary % (int)Math.Pow(10, digits);
            return password;
        }
    }
}
