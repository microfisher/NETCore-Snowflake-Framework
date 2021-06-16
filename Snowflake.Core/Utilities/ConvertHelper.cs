using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Core.Utilities
{
    public static class ConvertHelper
    {
        /// <summary>
        /// 四舍五入
        /// </summary>
        public static decimal Round(decimal number, int count = 5)
        {
            return Math.Round(number, count, MidpointRounding.AwayFromZero);
        }


    }
}
