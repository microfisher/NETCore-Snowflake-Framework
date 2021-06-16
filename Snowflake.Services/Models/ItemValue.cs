using System;
namespace Snowflake.Services.Models
{
    public class ItemValue
    {
        public string Item { get; set; }

        public object Value { get; set; }

        public ItemValue(string item, object value)
        {
            Item = item;
            Value = value;
        }
    }
}
