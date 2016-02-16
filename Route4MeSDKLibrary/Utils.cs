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
        writer.WriteObject(memoryStream, obj);

        result = Encoding.Default.GetString(memoryStream.ToArray());
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
      TimeSpan diff = date.ToUniversalTime() - origin;
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
  }
}
