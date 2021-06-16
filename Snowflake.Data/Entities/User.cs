using System;
using System.Collections.Generic;

namespace Snowflake.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public int Level { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public bool IsDisable { get; set; }
        public string IpAddress { get; set; }
        public long SignInTime { get; set; }
        public long SignUpTime { get; set; }
    }
}
