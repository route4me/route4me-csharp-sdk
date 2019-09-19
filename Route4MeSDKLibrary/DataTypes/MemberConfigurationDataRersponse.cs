using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response structure for the member's configuration data request
    /// </summary>
    [DataContract]
    public sealed class MemberConfigurationDataResponse
    {
        /// <summary>
        /// The member's configuration key
        /// </summary>
        [DataMember(Name = "config_key", EmitDefaultValue = false)]
        public string result { get; set; }

        /// <summary>
        /// The member's configutration data. See <see cref="MemberConfigurationData"/>
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public MemberConfigurationData[] data { get; set; }
    }
}