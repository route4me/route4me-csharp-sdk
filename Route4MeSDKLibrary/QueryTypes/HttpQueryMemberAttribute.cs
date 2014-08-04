using System;

namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Property attribute that is used by GenericParameters class
  /// Only properties with this attribute in GenericParameters or derived class are serialized by GenericParameters.Serialize()
  /// See example in Route4MeExamples.SetGPSPosition()
  /// </summary>
  public sealed class HttpQueryMemberAttribute : Attribute
  {
    #region Properties

    /// <summary>
    /// The serialized argument name, if specifed overrides the property name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Specifies whether to emit the property value, if its value is a default value
    /// </summary>
    public bool EmitDefaultValue { get; set; }

    /// <summary>
    /// Specifies the default value, that is used when emiting the property value
    /// If not specified null is used as a default value
    /// </summary>
    public object DefaultValue { get; set; }

    #endregion

    #region Methods

    public HttpQueryMemberAttribute()
    {
      EmitDefaultValue = true;
    }

    #endregion
  }
}
