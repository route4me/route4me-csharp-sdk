using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;

namespace Route4MeSDK
{
    /// <summary>
    /// Route4Me C# SDK helper methods
    /// </summary>
    public static class R4MeUtils
    {
        /// <summary>
        /// List of the standard types
        /// </summary>
        static List<string> lsStandardTypes = new List<string>()
        {
            {"String"},{"Boolean"},{"String[]"},{"Nullable`1"},{"Int32"},{"Double"},{"Int16"},{"Int64"},{"Single"},{"Decimal"}
        };

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

        public static T ReadObjectNew<T>(this Stream stream)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                //NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include,
                //DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new DataContractResolver()
            };


            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(text, jsonSettings);
        }

        public static T ReadObjectNew<T>(string jsonText)
        {

            var jsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DataContractResolver()
            };

            //StreamReader reader = new StreamReader(stream);
            //string text = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(jsonText, jsonSettings);
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
                try
                {
                    writer.WriteObject(memoryStream, obj);
                }
                catch (Exception ex)
                {
                    result = SerializeObjectToJson(obj, true);
                    return result;
                }

                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return result;
        }

        /// <summary>
        /// Serializes an object with/without null values.
        /// </summary>
        /// <param name="obj">An object</param>
        /// <param name="ignoreNullValues">If true, the null values will be ignored</param>
        /// <returns>Serialized JSON string</returns>
        public static string SerializeObjectToJson(object obj, bool ignoreNullValues)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                //NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include,
                //DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new DataContractResolver()
            };

            string result = JsonConvert.SerializeObject(obj, Formatting.None, jsonSettings);

            return result;
        }

        /// <summary>
        /// Returns the DescriptionAttribute of a enum value
        /// </summary>
        public static string Description(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

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
        /// Creates deep clone of the Route4Me object
        /// </summary>
        /// <typeparam name="T">Route4Me object type</typeparam>
        /// <param name="obj">Route4Me object</param>
        /// <returns>Route4Me object clone</returns>
        public static T ObjectDeepClone<T>(T obj) where T : class
        {
            T clonedObject = null;

            try
            {
                var jsonString = SerializeObjectToJson(obj, false);

                var stream = StringToStream(jsonString);

                clonedObject = ReadObjectNew<T>(stream);
            }
            catch (Exception)
            {
                clonedObject = null;
            }

            return clonedObject;
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Compares two Route4Me objects with equal types and returns a list 
        /// of the property names with different values.
        /// </summary>
        /// <param name="modifiedObject">Modified Route4Me object</param>
        /// <param name="initialObject">Initial Route4Me object</param>
        /// <param name="errorString">Error string</param>
        /// <returns>List of the property names</returns>
        public static List<string> GetPropertiesWithDifferentValues(object modifiedObject, object initialObject, out string errorString, bool excludeReadonly = true)
        {
            errorString = "";

            if (modifiedObject == null)
            {
                errorString = "The modified object should not be null";
                return null;
            }

            var propNames = new List<string>();
            var properties = modifiedObject.GetType().GetProperties();

            // If an initial object is not specified, a list of all property names will be returned.
            if (initialObject == null)
            {
                return properties.Select(x => x.Name).ToList();
            }

            if (modifiedObject.GetType() != initialObject.GetType())
            {
                errorString = "The objects should have equal types";
                return null;
            }

            foreach (var propInfo in properties)
            {
                if (CheckIfPropertyHasIgnoreAttribute(propInfo)) continue;
                if (excludeReadonly && CheckIfPropertyHasReadOnlyAttribute(propInfo)) continue;

                object modifiedObjectPropertyValue = propInfo.GetValue(modifiedObject);
                object initialObjectPropertyValue = propInfo.GetValue(initialObject);

                if (modifiedObjectPropertyValue == null)
                {
                    if (initialObjectPropertyValue != null) propNames.Add(propInfo.Name);
                    continue;
                }

                if (initialObjectPropertyValue == null)
                {
                    if (modifiedObjectPropertyValue != null) propNames.Add(propInfo.Name);
                    continue;
                }

                if (propInfo.PropertyType.IsArray)
                {
                    bool equalArrays = false;

                    try
                    {
                        var modifiedArray = (Array)modifiedObjectPropertyValue;
                        var initialArray = (Array)initialObjectPropertyValue;

                        if (modifiedArray.Length < 1 || initialArray.Length < 1) continue;

                        if (modifiedArray.Length != initialArray.Length)
                        {
                            propNames.Add(propInfo.Name);
                            continue;
                        }

                        for (int i=0; i< initialArray.Length; i++)
                        {
                            object objectItemInitial = (object)(initialArray.GetValue(i));
                            object objectItemModified = (object)(modifiedArray.GetValue(i));

                            if (objectItemInitial.Equals(objectItemModified))
                            {
                                equalArrays = true;
                            }
                            else
                            {
                                equalArrays = false;
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (!equalArrays) propNames.Add(propInfo.Name);
                }

                if (IsPropertyDictionary(initialObjectPropertyValue))
                {
                    var modifiedDict = (Dictionary<string, string>)modifiedObjectPropertyValue;
                    var initialDict = (Dictionary<string, string>)initialObjectPropertyValue;

                    if (!IsDictionariesEqual(initialDict, modifiedDict))
                    {
                        propNames.Add(propInfo.Name);
                    }

                    continue;
                }

                if (IsPropertyObject(initialObjectPropertyValue))
                {
                    Console.WriteLine("Object");
                }

                if (!modifiedObjectPropertyValue.Equals(initialObjectPropertyValue))
                {
                    propNames.Add(propInfo.Name);
                }
            }

            return propNames;
        }

        /// <summary>
        /// Checks if the property value is Dictionary type.
        /// </summary>
        /// <param name="propValue">The property value</param>
        /// <returns>True, if the property value is the Dictionary type.</returns>
        public static bool IsPropertyDictionary(object propValue)
        {
            if (propValue == null) return false;

            bool isDictionary = propValue is IDictionary;
            bool isGenericType = isDictionary && propValue.GetType().IsGenericType;

            return isDictionary && isGenericType;
        }

        /// <summary>
        /// Checks if the property value is Object type.
        /// </summary>
        /// <param name="propValue">The property value</param>
        /// <returns>True, if the property value is Object type.</returns>
        public static bool IsPropertyObject(object propValue)
        {
            if (propValue == null) return false;

            if (IsPropertyArray(propValue)) return false;

            if (IsPropertyDictionary(propValue)) return false;

            if (propValue.GetType().IsClass)
            {
                string propType = propValue.GetType().ToString().Replace("System.", "");
                return lsStandardTypes.Contains(propType) ? false : true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the property value is Array type.
        /// </summary>
        /// <param name="propValue">The property value</param>
        /// <returns>True, if the property value is Array type.</returns>
        public static bool IsPropertyArray(object propValue)
        {
            if (propValue == null) return false;

            return propValue.GetType().IsArray;
        }

        /// <summary>
        /// Compares two dictionaries if they are equal.
        /// </summary>
        /// <param name="x">First dictionary</param>
        /// <param name="y">Second dictionary</param>
        /// <returns>True, if the dictionaries are equal</returns>
        static bool IsDictionariesEqual(Dictionary<string, string> x, Dictionary<string, string> y)
        {
            if (x == y)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            bool result = false;

            result = x.Count == y.Count;

            if (result)
            {
                foreach (KeyValuePair<string, string> xKvp in x)
                {
                    string yValue;

                    if (!y.TryGetValue(xKvp.Key, out yValue))
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        result = xKvp.Value.Equals(yValue);
                        if (!result)
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if a property has attribute IgnoreDataMember
        /// </summary>
        /// <param name="propInfo">A property to be checked</param>
        /// <returns>True if the property has attribute IgnoreDataMember</returns>
        public static bool CheckIfPropertyHasIgnoreAttribute(PropertyInfo propInfo)
        {
            var ignoreProperties = propInfo.GetCustomAttributes(false).ToDictionary(a => a.GetType().Name, a => a); ;

            return ignoreProperties.Keys.Contains("IgnoreDataMemberAttribute") ? true : false;
        }

        /// <summary>
        /// Checks if a Route4Me object property has read-only attribute.
        /// </summary>
        /// <param name="propInfo">Route4Me object property info</param>
        /// <returns>True, if a Route4Me object property is read-only </returns>
        public static bool CheckIfPropertyHasReadOnlyAttribute(PropertyInfo propInfo)
        {
            var attributes = propInfo.GetCustomAttributes(false).ToDictionary(a => a.GetType().Name, a => a);

            if (!attributes.ContainsKey("ReadOnlyAttribute")) return false;

            attributes.TryGetValue("ReadOnlyAttribute", out object isReadOnly);

            var isReadOnlyValue = isReadOnly != null ? ((Route4MeSDK.DataTypes.ReadOnlyAttribute)isReadOnly).IsReadOnly : false;

            return isReadOnlyValue;
        }

        /// <summary>
        /// Returns numeration of the Route4Me object proeprties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, int> GetPropertyPositions<T>() where T : class
        {
            var properties = typeof(T).GetProperties();
            var propertyPositions = new Dictionary<string, int>();

            for (int i = 0; i < properties.Length; i++)
            {
                propertyPositions.Add(properties[i].Name, i);
            }

            return propertyPositions;
        }

        /// <summary>
        /// Returns ordered property names.
        /// </summary>
        /// <typeparam name="T">Type of the Route4Me object</typeparam>
        /// <param name="propertyNames">List of the property names</param>
        /// <returns>Ordered list of the property names</returns>
        public static List<string> OrderPropertiesByPosition<T>(List<string> propertyNames, out string errorString) where T : class
        {
            errorString = "";

            var propertyPositions = GetPropertyPositions<T>();

            var orderedPropertyNames = new List<string>();

            foreach (var propKey in propertyPositions.Keys)
            {
                foreach (var propName in propertyNames)
                {
                    if (propKey == propName)
                    {
                        orderedPropertyNames.Add(propKey);
                        break;
                    }
                }

                if (orderedPropertyNames.Count== propertyNames.Count) break;
            }

            if (orderedPropertyNames.Count < propertyNames.Count) errorString = "Some of the properties have the wrong name";

            return orderedPropertyNames;
        }

        /// <summary>
        /// Converts an input object to type TValue.
        /// </summary>
        /// <typeparam name="TValue">Target type</typeparam>
        /// <param name="obj">An object to be converted to a TValue type</param>
        /// <returns>An object of TValue type</returns>
        public static TValue ToObject<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var objectValue = JsonConvert.DeserializeObject<TValue>(json);
            return objectValue;
        }
    }
}
