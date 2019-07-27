using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Route4MeSDK
{
    /// <summary>
    /// Route4Me C# SDK helper methods
    /// </summary>
    public static class R4MeUtils
    {
        /// <summary>
        /// Reads JSON object for a stream
        /// Any DataContractJsonSerializer can be thrown
        /// </summary>
        public static T ReadObject<T>(this Stream stream)
        {
            var settings = new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true
            };
            var parser = new DataContractJsonSerializer(typeof(T), settings);

            return (T)parser.ReadObject(stream);
        }

        /// <summary>
        /// Reads a stream to a string
        /// </summary>
        public static string ReadString(this Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            string result = reader.ReadToEnd();

            return result;
        }

        /// <summary>
        /// Serialized an object to a string as JSON
        /// Any DataContractJsonSerializer can be thrown
        /// </summary>
        public static string SerializeObjectToJson(object obj)
        {
            var settings = new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true
            };
            var writer = new DataContractJsonSerializer(obj.GetType(), settings);
            string result = null;

            using (var memoryStream = new MemoryStream())
            {
                if (obj == null) return result;
                writer.WriteObject(memoryStream, obj);

                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return result;
        }

        /// <summary>
        /// Returns the DescriptionAttribute of a enum value
        /// </summary>
        public static string Description(this Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            string result = attribute == null ? enumValue.ToString() : attribute.Description;

            return result;
        }

        /// <summary>
        /// IEnumerable extension method, performs 'action' for each IEnumerable item
        /// source value can be null
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }

            foreach (var item in source)
                action(item);
        }

        /// <summary>
        /// Convert DateTime to Unix epoch time
        /// </summary>
        public static long ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (date < origin) date = new DateTime(1970, 1, 1, date.Hour, date.Minute, date.Second);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Convert DateTime from Unix epoch time
        /// </summary>
        public static DateTime ConvertFromUnixTimestamp(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// Valiates the standard object value.
        /// </summary>
        /// <param name="value">The standard object value.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True, if the object is valide.</returns>
        public static bool ValiateStandardObjectValue(object value, string objectType)
        {
            switch (objectType)
            {
                case "Int16":
                    Int16 i16;
                    return Int16.TryParse(value.ToString(), out i16);
                case "Int32":
                    Int32 i32;
                    return Int32.TryParse(value.ToString(), out i32);
                case "Int64":
                    Int64 i64;
                    return Int64.TryParse(value.ToString(), out i64);
                case "UInt16":
                    UInt16 ui64;
                    return UInt16.TryParse(value.ToString(), out ui64);
                case "UInt32":
                    UInt32 ui32;
                    return UInt32.TryParse(value.ToString(), out ui32);
                case "Decimal":
                    Decimal dc;
                    return Decimal.TryParse(value.ToString(), out dc);
                case "Double":
                    Double dbl;
                    return Double.TryParse(value.ToString(), out dbl);
                case "Boolean":
                    Boolean bl;
                    return Boolean.TryParse(value.ToString(), out bl);
                case "DateTime":
                    DateTime dt;
                    return DateTime.TryParse(value.ToString(), out dt);
            }

            return false;
        }
    }
}
