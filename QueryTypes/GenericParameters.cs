using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Helper class, for easy REST query parameters string generation
  /// 1. Use GenericParameters.Serialize() to generate the query string
  /// 2. Use GenericParameters.ParametersCollection for adding query parameters
  /// 3. Inherit this class, to create usable parameters holders
  ///    Add an attribute [HttpQueryMemberAttribute] on each property for serializing it automatically
  /// 4. Modify ConvertBooleansToInteger.GenericParameters to serialize bool and bool? as "0" and "1"
  /// Important: You have to add here all derived classes, that are serealized as json as a KnownType
  /// </summary>
  [DataContract]
  [KnownType(typeof(OptimizatonParameters))]
  public class GenericParameters
  {
    #region Fields

    [IgnoreDataMember]
    public readonly NameValueCollection ParametersCollection;

    [IgnoreDataMember]
    public bool ConvertBooleansToInteger {get; protected set;}

    #endregion

    #region Methods

    public GenericParameters()
    {
      ConvertBooleansToInteger = true;
      ParametersCollection = new NameValueCollection();
    }

    public string Serialize(string apiKey = null)
    {
      var paramsCollection = HttpUtility.ParseQueryString(string.Empty);

      paramsCollection.Add(ParametersCollection);

      var properties = GetType().GetProperties();

      foreach (var property in properties)
      {
        var attribute = property.GetCustomAttribute(typeof(HttpQueryMemberAttribute)) as HttpQueryMemberAttribute;

        if (attribute != null)
        {
          var valueObj = property.GetValue(this);
          var value = valueObj != null ? valueObj.ToString() : "null";

          if (ConvertBooleansToInteger &&
              valueObj != null &&
              (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?)))
          {
            value = ((bool)valueObj) ? "1" : "0";
          }

          var name = attribute.Name ?? property.Name;
          var emit = attribute.IsRequired ||
                     valueObj != attribute.DefaultValue ||
                     attribute.EmitDefaultValue;

          if (emit)
          {
            paramsCollection.Add(name, value);
          }
        }
      }

      string apiKeyStr = string.IsNullOrEmpty(apiKey) ? "?" : string.Format("?api_key={0}", apiKey);
      string result = paramsCollection.Count > 0 ? string.Format("{0}&{1}", apiKeyStr, paramsCollection.ToString()) : apiKeyStr;

      return result;
    }

    #endregion
  }
}
