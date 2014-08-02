using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Route4MeSDK
{
  public static class R4MeUtils
  {
    internal static T ReadObject<T>(this Stream stream)
    {
      var parser = new DataContractJsonSerializer(typeof(T));

      return (T)parser.ReadObject(stream);
    }

    internal static string ReadString(this Stream stream)
    {
      StreamReader reader = new StreamReader(stream);

      string result = reader.ReadToEnd();

      return result;
    }

    internal static string SerializeObjectToJson(object obj)
    {
      var writer = new DataContractJsonSerializer(obj.GetType());
      string result = null;

      using (var memoryStream = new MemoryStream())
      {
        writer.WriteObject(memoryStream, obj);

        result = Encoding.Default.GetString(memoryStream.ToArray());
      }

      return result;
    }

    internal static string Description(this Enum enumValue)
    {
      FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());

      var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

      string result = attribute == null ? enumValue.ToString() : attribute.Description;

      return result;
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
        action(item);
    }
  }
}
