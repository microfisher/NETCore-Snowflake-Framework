using System;
namespace Snowflake.Data.Entities
{
    public class UserBalance
    {
        public long Id { get; set; }

        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
    }
}
