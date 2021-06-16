using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Core.Responses
{
    public interface IResultObject
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        dynamic Data { get; set; }


    }

    public interface IResultObject<T>
    {

        /// <summary>
        /// 返回状态
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        T Data { get; set; }
    }


}
