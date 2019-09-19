using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the member configuration.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class MemberConfigurationParameters : GenericParameters
    {
        /// <summary>
        /// Config key of a member.
        /// </summary>
        [DataMember(Name = "config_key", EmitDefaultValue = false)]
        public string ConfigKey { get; set; }

        /// <summary>
        /// Config value of a member.
        /// </summary>
        [DataMember(Name = "config_value", EmitDefaultValue = false)]
        public string ConfigValue { get; set; }
    }
}
