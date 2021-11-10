using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     For creating dynamic Route4Me data classes with the specified properties only.
    ///     For resolving update problem: if not serialize null values, it's impossible to set null,
    ///     if enable null values, actual values could be rewritten with null values.
    /// </summary>
    public class Route4MeDynamicClass : DynamicObject
    {
        /// <summary>
        ///     Getter of the dynamic properties
        /// </summary>
        public Dictionary<string, object> DynamicProperties { get; } = new Dictionary<string, object>();

        /// <summary>
        ///     Tries to set member
        /// </summary>
        /// <param name="binder">Member binder</param>
        /// <param name="value">Object value</param>
        /// <returns>True, if a member binded successfully</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            DynamicProperties.Add(binder.Name, value);

            // additional error checking code omitted

            return true;
        }


        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return DynamicProperties.TryGetValue(binder.Name, out result);
        }

        /// <summary>
        ///     Tries to set member
        /// </summary>
        /// <returns>Multiline property name & value pairs</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var property in DynamicProperties)
                sb.AppendLine($"Property '{property.Key}' = '{property.Value}'");

            return sb.ToString();
        }

        /// <summary>
        ///     Copy a specified list of the properties of the Route4Me object to the dynamic class.
        /// </summary>
        /// <param name="r4mObject">Route4Me object</param>
        /// <param name="propertyNames">A specified list of the Route4Me class properties</param>
        /// <param name="errorString">Error string</param>
        public void CopyPropertiesFromClass(object r4mObject, List<string> propertyNames, out string errorString)
        {
            errorString = "";

            foreach (var propertyName in propertyNames)
            {
                var propInfo = r4mObject?.GetType()?.GetProperty(propertyName) ?? null;
                if (propInfo == null) continue;

                var customAttribute = propInfo?.CustomAttributes?.First() ?? null;
                if (customAttribute == null) continue;

                var typedValue = customAttribute?.NamedArguments?.FirstOrDefault().TypedValue.Value?.ToString() ?? null;
                if (typedValue == null) continue;

                if (typedValue == "IgnoreDataMemberAttribute") continue;

                if (!DynamicProperties.ContainsKey(typedValue))
                    DynamicProperties.Add(typedValue,
                        r4mObject.GetType().GetProperty(propertyName).GetValue(r4mObject));
            }
        }
    }
}