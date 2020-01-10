using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class EncryptHelper
    {
        public static readonly string SYSTEM_ENCRYPT_SECRET = ".Microfisher.Reactor.App";

        public static readonly string SYSTEM_BASE32_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        /// <summary>
        /// 按ID生成随机码
        /// </summary>
        public static string RandomKey(long id, int length = 6, bool isNumber = false)
        {
            var rnd = new Random();
            var builder = new StringBuilder();
            var data = Convert.ToString(id, 16).ToUpper();
            var source = isNumber ? "234567898765432345678987654323456789" : "AZ2BY3CX4DW5EV6FU7GT8HS9JR2QK3PL4NM5";

            for (var i = 0; i < data.Length; i++)
            {
                var x = source.Substring(rnd.Next(0, source.Length - 1), 1);
                builder.Append(data[i] + x);
            }

            var len = builder.Length;
            for (var i = 0; i < length - len; i++)
            {
                var x = source.Substring(rnd.Next(0, source.Length - 1), 1);
                builder.Append(x);
            }

            return builder.ToString();
        }

        /// <summary>
        /// URL加密
        /// </summary>
        public static string UrlEncode(string value)
        {
            var result = new StringBuilder();
            var validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            foreach (var symbol in value)
                if (validChars.IndexOf(symbol) != -1)
                    result.Append(symbol);
                else
                    result.Append('%' + string.Format("{0:X2}", (int)symbol));

            return result.ToString().Replace(" ", "%20");
        }

        /// <summary>
        /// MD5加密字符串
        /// </summary>
        public static string Md5Encrypt(string text)
        {
            var data = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text + SYSTEM_ENCRYPT_SECRET));
                for (var i = 0; i < hashBytes.Length; i++)
                {
                    data.Append(hashBytes[i].ToString("X2"));
                }
            }
            return data.ToString();
        }

        /// <summary>
        /// Sha256加密字符串
        /// </summary>
        public static string Sha256Encrypt(string text)
        {
            var data = string.Empty;
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                data = BitConverter.ToString(bytes).Replace("-", "");
            }
            return data.ToLower();
        }

        /// <summary>
        /// HmacSha256加密
        /// </summary>
        public static string HmacSha256Encrypt(string text, string secrectKey)
        {
            var data = string.Empty;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secrectKey)))
            {
                hmac.Initialize();
                var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
                data = BitConverter.ToString(bytes).Replace("-", "");
            }
            return data;
        }



        /// <summary>
        /// Base32加密
        /// </summary>
        public static string Base32Encrypt(string data)
        {
            var text = Encoding.UTF8.GetBytes(data);
            var output = "";
            for (var bitIndex = 0; bitIndex < text.Length * 8; bitIndex += 5)
            {
                var dualbyte = text[bitIndex / 8] << 8;
                if (bitIndex / 8 + 1 < text.Length)
                    dualbyte |= text[bitIndex / 8 + 1];
                dualbyte = 0x1f & (dualbyte >> (16 - bitIndex % 8 - 5));
                output += SYSTEM_BASE32_CHARS[dualbyte];
            }

            return output;
        }

        /// <summary>
        /// Base32解密
        /// </summary>
        public static string Base32Decrypt(string data)
        {
            var output = new List<byte>();
            var bytes = data.ToCharArray();
            for (var bitIndex = 0; bitIndex < data.Length * 5; bitIndex += 8)
            {
                var dualbyte = SYSTEM_BASE32_CHARS.IndexOf(bytes[bitIndex / 5]) << 10;
                if (bitIndex / 5 + 1 < bytes.Length)
                    dualbyte |= SYSTEM_BASE32_CHARS.IndexOf(bytes[bitIndex / 5 + 1]) << 5;
                if (bitIndex / 5 + 2 < bytes.Length)
                    dualbyte |= SYSTEM_BASE32_CHARS.IndexOf(bytes[bitIndex / 5 + 2]);

                dualbyte = 0xff & (dualbyte >> (15 - bitIndex % 5 - 8));
                output.Add((byte)dualbyte);
            }

            var key = Encoding.UTF8.GetString(output.ToArray());
            if (key.EndsWith("\0"))
            {
                var index = key.IndexOf("\0", StringComparison.Ordinal);
                key = key.Remove(index, 1);
            }

            return key;
        }


        /// <summary>
        /// Base64加密
        /// </summary>
        public static string Base64Encrypt(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        public static string Base64Decrypt(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }



        /// <summary>
        /// AES加密
        /// </summary>
        public static string AESEncrypt(string text, string password)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                        cs.Close();
                    }
                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return text;
        }
        public static string AESEncrypt(string text)
        {
            return AESEncrypt(text, SYSTEM_ENCRYPT_SECRET);
        }


        /// <summary>
        /// AES解密
        /// </summary>
        public static string AESDecrypt(string text, string password)
        {
            var bytes = Convert.FromBase64String(text);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                        cs.Close();
                    }
                    text = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return text;
        }
        public static string AESDecrypt(string text)
        {
            return AESDecrypt(text, SYSTEM_ENCRYPT_SECRET);
        }

    }
}
