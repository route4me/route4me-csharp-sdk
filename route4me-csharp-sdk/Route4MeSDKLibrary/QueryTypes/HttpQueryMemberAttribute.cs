using System;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Helper class for the HTTP query request
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public sealed class HttpQueryMemberAttribute : Attribute
    {
        #region Methods

        /// <summary>Initializes a new instance of the <see cref="HttpQueryMemberAttribute" /> class.</summary>
        public HttpQueryMemberAttribute()
        {
            EmitDefaultValue = true;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The serialized argument name, if specifed overrides the property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Specifies whether to emit the property value, if its value is a default value
        /// </summary>
        public bool EmitDefaultValue { get; set; }

        /// <summary>
        ///     Specifies the default value, that is used when emiting the property value
        ///     If not specified null is used as a default value
        /// </summary>
        public object DefaultValue { get; set; }

        #endregion
    }
}