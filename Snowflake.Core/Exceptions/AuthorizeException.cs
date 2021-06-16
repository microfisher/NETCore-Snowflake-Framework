using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Core.Exceptions
{
    [Serializable]
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string message) : base(message) { }

        public AuthorizeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
