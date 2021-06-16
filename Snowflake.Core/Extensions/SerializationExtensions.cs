using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Snowflake.Core.Extensions
{
    public static class SerializationExtensions
    {
        public static byte[] ToByteArray(this object value)
        {
            if (value == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                //ProtoBuf.Serializer.Serialize(stream, value);
                //return stream.ToArray();
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                return stream.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] bytes) where T : class
        {
            if (bytes == null)
            {
                return default(T);
            }

            using (var stream = new MemoryStream(bytes))
            {
                //return ProtoBuf.Serializer.Deserialize<T>(stream);
                var formatter = new BinaryFormatter();
                var target = formatter.Deserialize(stream);
                return target as T;
            }
        }

    }
}
