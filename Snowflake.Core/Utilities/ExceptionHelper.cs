using Snowflake.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Core.Utilities
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// 抛出消息
        /// </summary>
        /// <param name="message"></param>
        public static void Throw(string message)
        {
            throw new MessageException(message);
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="ex"></param>
        public static void Throw(Exception ex)
        {
            if (ex is MessageException || ex is AuthorizeException)
            {
                throw ex;
            }
            else
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
