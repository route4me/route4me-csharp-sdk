using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using System.Configuration;
using Route4MeSDK.DataTypes;
using Microsoft.CSharp;
using static Route4MeSDK.Route4MeManager;
using System.Linq.Expressions;

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
            
            if (typeof(T)==typeof(GetAddressBookContactsResponse))
            {
                string pattern = String.Concat(
                    "\\\"schedule\\\"",
                    @":({[\s\S\n\d\w]*}),",
                    "\"");
                Regex rgx = new Regex(pattern);
                Match rslt = rgx.Match(text);

                if (rslt.Success)
                {
                    text = text.Replace(rslt.Groups[1].ToString(), "[" + rslt.Groups[1] + "]");
                }
            }
            
            return JsonConvert.DeserializeObject<T>(text, jsonSettings);
        }

        public static T ReadObjectNew<T>(string jsonText)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DataContractResolver()
            };

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
                catch (Exception)
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

        public static string SerializeObjectToJson(object obj, string[] mandatoryFields)
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
        /// Convert the dd:HH:mm format string to the seconts (int)
        /// </summary>
        /// <param name="ddhhmm">The dd:HH:mm format string</param>
        /// <param name="errorString">Error string</param>
        /// <returns>Seconds</returns>
        public static int? DDHHMM2Seconds(string ddhhmm, out string errorString)
        {
            errorString = "";

            if (ddhhmm == null)
            {
                errorString = "Wrong time.Specify the time in the format HH: mm: ss";
                return null;
            }
            string regexPattern = @"\d{2}\:[0-2][0-9]\:[0-6][0-9]";
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(ddhhmm);

            if (match.Success)
            {
                string[] parts = ddhhmm.Split(':');
                int days = Convert.ToInt32(parts[0]);
                int hours = Convert.ToInt32(parts[1]);
                int minutes = Convert.ToInt32(parts[2]);

                return days * 24 * 3600 + hours * 3600 + minutes * 60;
            }
            else
            {
                errorString = "Wrong time.Specify the time in the format HH: mm: ss";
                return null;
            }
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
            var propNames = new List<string>();
            errorString = "";

            try
            {
                var jsonModifiedObject = JsonConvert.SerializeObject(modifiedObject);
                var jsonInitialObject = JsonConvert.SerializeObject(initialObject);

                if (jsonModifiedObject.Equals(jsonInitialObject)) return propNames;
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
                return propNames;
            }

            if (modifiedObject == null)
            {
                errorString = "The modified object should not be null";
                return null;
            }
            
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

                try
                {
                    var jsonModifiedObjectPropertyValue = JsonConvert.SerializeObject(modifiedObjectPropertyValue);
                    var jsonInitialObjectPropertyValue = JsonConvert.SerializeObject(initialObjectPropertyValue);

                    if (jsonModifiedObjectPropertyValue.Equals(jsonInitialObjectPropertyValue)) continue;
                }
                catch (Exception)
                {
                    continue;
                }

                propNames.Add(propInfo.Name);
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
        /// <typeparam name="T">Class type</typeparam>
        /// <returns>Property postions in the class</returns>
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

        // <summary>
        // Get the name of a static or instance property from a property access lambda.
        // </summary>
        // <typeparam name="T">Type of the property</typeparam>
        // <param name="propertyLambda">lambda expression of the form: '() => Class.Property' or '() => object.Property'</param>
        // <returns>The name of the property</returns>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
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
        public static TValue ToObject<TValue>(object obj, out string errorString)
        {
            errorString = "";

            if (obj == null) return default(TValue);

            try
            {
                var json = JsonConvert.SerializeObject(obj);
                if (json == "[]") return default(TValue);

                var objectValue = JsonConvert.DeserializeObject<TValue>(json);
                return objectValue;
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
                return default(TValue);
            }
        }

        /// <summary>
        /// Converts one standard type object to other.
        /// </summary>
        /// <typeparam name="T">Destincation type for converting to</typeparam>
        /// <param name="value">INput value of the object type</param>
        /// <returns>Converted value of the type T</returns>
        public static T ConvertObjectToType<T>(ref object value)
            where T : struct
        {
            T result = default(T);

            if (value == null) return result;

            Type destinationType = result.GetType();

            if (Nullable.GetUnderlyingType(destinationType) != null) destinationType = Nullable.GetUnderlyingType(destinationType);

            Type convertObjectType = value?.GetType() ?? null;

            if (destinationType == null || convertObjectType == null) return result;

            if (destinationType == typeof(object)) return result;

            if (value is IConvertible)
            {
                try
                {
                    if (destinationType == typeof(Boolean))
                    {
                        result = (T)(object)((IConvertible)value).ToBoolean(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Byte))
                    {
                        result = (T)(object)((IConvertible)value).ToByte(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Char))
                    {
                        result = (T)(object)((IConvertible)value).ToChar(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(DateTime))
                    {
                        result = (T)(object)((IConvertible)value).ToDateTime(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Decimal))
                    {
                        result = (T)(object)((IConvertible)value).ToDecimal(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Double))
                    {
                        result = (T)(object)((IConvertible)value).ToDouble(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Int16))
                    {
                        result = (T)(object)((IConvertible)value).ToInt16(CultureInfo.CurrentCulture);
                        //return true;
                    }
                    else if (destinationType == typeof(Int32))
                    {
                        result = (T)(object)((IConvertible)value).ToInt32(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Int64))
                    {
                        result = (T)(object)((IConvertible)value).ToInt64(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(SByte))
                    {
                        result = (T)(object)((IConvertible)value).ToSByte(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Single))
                    {
                        result = (T)(object)((IConvertible)value).ToSingle(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt16))
                    {
                        result = (T)(object)((IConvertible)value).ToUInt16(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt32))
                    {
                        result = (T)(object)((IConvertible)value).ToUInt32(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt64))
                    {
                        result = (T)(object)((IConvertible)value).ToUInt64(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(String))
                    {
                        result = (T)(object)((IConvertible)value).ToString(CultureInfo.CurrentCulture);
                    }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a standard type object to standard property type 
        /// (e.g. if first is Long type, second: Int32, converts to Int32)
        /// </summary>
        /// <param name="convertObject">An object to be converted to the target object type</param>
        /// <param name="targetProperty">A property with the standard type</param>
        /// <returns>Converted object to the target standard type</returns>
        public static object ConvertObjectToPropertyType(object value, PropertyInfo targetProperty)
        {
            Type destinationType = targetProperty?.PropertyType ?? null;

            if (Nullable.GetUnderlyingType(targetProperty.PropertyType) != null) destinationType = Nullable.GetUnderlyingType(targetProperty.PropertyType);

            Type convertObjectType = value?.GetType() ?? null;

            if (destinationType == null || convertObjectType == null) return null;

            if (targetProperty.PropertyType.Name.ToLower().Contains("dictionary")) return value;
            // Non-standard object isn't converted

            if (destinationType == typeof(object))
            {
                if (IsPropertyDictionary(value))
                {
                    value = (Dictionary<string, string>)((object)value);
                    return value;

                }
                else return null;
            }

            object result = null;

            if (destinationType.BaseType.Name == "Enum" || destinationType.BaseType.Name == "Array")
            {
                return value;
            }

            if (value is IConvertible)
            {
                try
                {
                    if (destinationType == typeof(Boolean))
                    {
                        result = ((IConvertible)value).ToBoolean(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Byte))
                    {
                        result = ((IConvertible)value).ToByte(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Char))
                    {
                        result = ((IConvertible)value).ToChar(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(DateTime))
                    {
                        result = ((IConvertible)value).ToDateTime(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Decimal))
                    {
                        result = ((IConvertible)value).ToDecimal(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Double))
                    {
                        result = ((IConvertible)value).ToDouble(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Int16))
                    {
                        result = ((IConvertible)value).ToInt16(CultureInfo.CurrentCulture);
                        return true;
                    }
                    else if (destinationType == typeof(Int32))
                    {
                        result = ((IConvertible)value).ToInt32(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Int64))
                    {
                        result = ((IConvertible)value).ToInt64(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(SByte))
                    {
                        result = ((IConvertible)value).ToSByte(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Single))
                    {
                        result = ((IConvertible)value).ToSingle(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt16))
                    {
                        result = ((IConvertible)value).ToUInt16(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt32))
                    {
                        result = ((IConvertible)value).ToUInt32(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt64))
                    {
                        result = ((IConvertible)value).ToUInt64(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(String))
                    {
                        result = ((IConvertible)value).ToString(CultureInfo.CurrentCulture);
                    }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Read a setting parameter from App.config by specified key
        /// </summary>
        /// <param name="key">A setting key</param>
        /// <returns>A seeting value</returns>
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key];

                if (result==null) return "Not found";

                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                return "Error reading app settings."+Environment.NewLine+ex.Message;
            }
        }

        /// <summary>
        /// Get local timezone in seconds.
        /// </summary>
        /// <returns>Timezone in seconds</returns>
        public static int GetLocalTimeZone()
        {
            var seconds = (int)TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalSeconds;

            return seconds;
        }

        /// <summary>
        /// Generate random string with a specified length from specified source string.
        /// </summary>
        /// <param name="length">Length of the generated random string</param>
        /// <param name="sourceString">Source string. Default: 'ABCDEF0123456789'</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length, string sourceString = "ABCDEF0123456789")
        {
            var random = new Random();

            return new string(Enumerable.Repeat(sourceString, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
