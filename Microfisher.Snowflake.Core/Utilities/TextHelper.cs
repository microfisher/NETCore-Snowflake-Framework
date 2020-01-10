using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class TextHelper
    {
        /// <summary>
        /// IP字符串转整数
        /// </summary>
        public static long IPAddressToNumber(string ip)
        {
            var separator = new char[] { '.' };
            var items = ip.Split(separator);
            return long.Parse(items[0]) << 24
                    | long.Parse(items[1]) << 16
                    | long.Parse(items[2]) << 8
                    | long.Parse(items[3]);
        }

        /// <summary>
        /// 整数转IP字符串
        /// </summary>
        public static string NumberToIPAddress(long ip)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((ip >> 24) & 0xFF).Append(".");
            sb.Append((ip >> 16) & 0xFF).Append(".");
            sb.Append((ip >> 8) & 0xFF).Append(".");
            sb.Append(ip & 0xFF);
            return sb.ToString();
        }

        /// <summary>
        /// HTML编码
        /// </summary>
        public static string HtmlEncode(string data)
        {
            return System.Net.WebUtility.HtmlEncode(data);
        }

        /// <summary>
        /// HTML解码
        /// </summary>
        public static string HtmlDecode(string data)
        {
            return System.Net.WebUtility.HtmlDecode(data);
        }

        /// <summary>
        /// 获取字符数量，中文算两个
        /// </summary>
        public static int GetLength(string s)
        {
            int length = 0;
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] >= 0x4E00 && charArray[i] <= 0x9FA5)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }


        /// <summary>
        /// 隐藏IP后两位
        /// </summary>
        public static string GetHideIP(string ipAddress)
        {
            string result = string.Empty;
            if (ipAddress.Contains(","))
            {
                result = ipAddress.Trim(' ').Trim('　').Split(',').First();
            }
            else
            {
                result = ipAddress.Trim(' ').Trim('　');
            }
            if (string.IsNullOrWhiteSpace(result) || !ValidateHelper.IsIPv4(result) || result == "0.0.0.0")
            {
                return string.Empty;
            }
            string[] nums = ipAddress.Split('.');
            if (nums.Length > 1)
            {
                result = $"{nums[0]}.{nums[1]}.*.*";
            }
            return result;
        }

        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        public static string GetNumber(string text)
        {
            var result = string.Empty;
            if (text != null && text != string.Empty)
            {
                text = Regex.Replace(text, @"[^\d.\d]", "");
                if (Regex.IsMatch(text, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = text;
                }
            }
            return result;
        }


        public static string Html2Text(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return "";
            }
            string regEx_style = "<style[^>]*?>[\\s\\S]*?<\\/style>"; //定义style的正则表达式 
            string regEx_script = "<script[^>]*?>[\\s\\S]*?<\\/script>"; //定义script的正则表达式 
            string regEx_html = "<[^>]+>"; //定义HTML标签的正则表达式 
            text = Regex.Replace(text, regEx_style, "");//删除css
            text = Regex.Replace(text, regEx_script, "");//删除js
            text = Regex.Replace(text, regEx_html, "");//删除html标记
            //text = Regex.Replace(text, "\\s*|\t|\r|\n", "");//去除tab、空格、空行
            //text = text.Replace(" ","");
            text = text.Replace("\"", "");
            text = text.Replace("\"", "");
            return text.Trim();
        }
    }
}
