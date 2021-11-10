using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Helper class, for easy REST query parameters string generation
    ///     1. Use GenericParameters.Serialize() to generate the query string
    ///     2. Use GenericParameters.ParametersCollection for adding query parameters
    ///     3. Inherit this class, to create usable parameters holders
    ///     Add an attribute [HttpQueryMemberAttribute] on each property for serializing it automatically
    ///     4. Modify ConvertBooleansToInteger.GenericParameters to serialize bool and bool? as "0" and "1"
    ///     Important: You have to add here all derived classes, that are serealized as json as a KnownType
    /// </summary>
    [DataContract]
    [KnownType(typeof(OptimizationParameters))]
    [KnownType(typeof(AddressBookContact))]
    [KnownType(typeof(ActivityParameters))]
    [KnownType(typeof(AddressBookParameters))]
    [KnownType(typeof(AddressParameters))]
    [KnownType(typeof(GPSParameters))]
    [KnownType(typeof(NoteParameters))]
    [KnownType(typeof(RouteParameters))]
    [KnownType(typeof(RouteParametersQuery))]
    [KnownType(typeof(AvoidanceZoneParameters))]
    [KnownType(typeof(AvoidanceZoneQuery))]
    [KnownType(typeof(GeocodingParameters))]
    [KnownType(typeof(MemberParameters))]
    [KnownType(typeof(MemberConfigurationParameters))]
    [KnownType(typeof(VehicleV4Parameters))]
    [KnownType(typeof(VehicleV4Response))]
    public class GenericParameters
    {
        #region Fields

        [IgnoreDataMember] public NameValueCollection ParametersCollection = new NameValueCollection();

        [IgnoreDataMember] public bool ConvertBooleansToInteger { get; protected set; }

        #endregion

        #region Methods

        /// <summary>Initializes a new instance of the <see cref="GenericParameters" /> class.</summary>
        public GenericParameters()
        {
            PrepareForSerialization();
        }

        /// <summary>
        ///     Prepares the parameters for serialization.
        /// </summary>
        public void PrepareForSerialization()
        {
            ConvertBooleansToInteger = true;
            if (ParametersCollection == null)
                ParametersCollection = new NameValueCollection();
        }

        /// <summary>
        ///     Serializes the parameters with an API key (if specified).
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>Serialized string</returns>
        public string Serialize(string apiKey = null)
        {
            var paramsCollection = HttpUtility.ParseQueryString(string.Empty);

            paramsCollection.Add(ParametersCollection);

            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute =
                    property.GetCustomAttribute(typeof(HttpQueryMemberAttribute)) as HttpQueryMemberAttribute;

                if (attribute != null)
                {
                    var valueObj = property.GetValue(this);
                    var value = valueObj != null ? valueObj.ToString() : "null";

                    if (ConvertBooleansToInteger &&
                        valueObj != null &&
                        (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?)))
                        value = (bool) valueObj ? "1" : "0";

                    var name = attribute.Name ?? property.Name;
                    var emit = valueObj != attribute.DefaultValue ||
                               attribute.EmitDefaultValue;

                    if (emit) paramsCollection.Add(name, value);
                }
            }

            var apiKeyStr = string.IsNullOrEmpty(apiKey) ? "?" : string.Format("?api_key={0}", apiKey);
            var result = paramsCollection.Count > 0 ? string.Format("{0}&{1}", apiKeyStr, paramsCollection) : apiKeyStr;

            return result;
        }

        #endregion
    }
}