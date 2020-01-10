using System;
using System.Collections.Generic;
using System.Text;

namespace Microfisher.Snowflake.Core.Responses
{
    public class ResultObject : IResultObject
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public dynamic Data { get; set; }

        public ResultObject(int status)
        {
            Status = status;
        }

        public ResultObject(int status, dynamic data)
        {
            Status = status;
            Data = data;
        }

    }

    public class ResultObject<T> : IResultObject<T>
    {

        /// <summary>
        /// 返回状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public T Data { get; set; } = default(T);

        public ResultObject(int status)
        {
            Status = status;
        }

        public ResultObject(int status, T data)
        {
            Status = status;
            Data = data;
        }
    }


}
