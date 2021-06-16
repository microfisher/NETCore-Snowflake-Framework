using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Snowflake.Core.Attributes;

namespace Snowflake.Core.Utilities
{
    public static class DataHelper
    {
        private static readonly ConcurrentDictionary<Type, List<string>> _parameterCache = new ConcurrentDictionary<Type, List<string>>();

        private static readonly ConcurrentDictionary<string, string> _sqlCache = new ConcurrentDictionary<string, string>();

        public static List<string> GetParamNames(object o)
        {

            if (!_parameterCache.TryGetValue(o.GetType(), out List<string> parameters))
            {
                parameters = new List<string>();
                foreach (var prop in o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.GetGetMethod(false) != null))
                {
                    var attribs = prop.GetCustomAttributes(typeof(IgnorePropertyAttribute), true);
                    var attr = attribs.FirstOrDefault() as IgnorePropertyAttribute;
                    if (attr == null || (!attr.Value))
                    {
                        parameters.Add(prop.Name);
                    }
                }
                _parameterCache[o.GetType()] = parameters;
            }
            return parameters;
        }
    }

}
