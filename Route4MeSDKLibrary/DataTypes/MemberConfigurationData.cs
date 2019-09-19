using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The member's confuguration data structure
    /// </summary>
    [DataContract]
    public sealed class MemberConfigurationData : GenericParameters
    {
        /// <summary>
        /// The member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int MemberId { get; set; }

        /// <summary>
        /// The member's config key
        /// </summary>
        [DataMember(Name = "config_key", EmitDefaultValue = false)]
        public string ConfigKey { get; set; }

        /// <summary>
        /// The member's config value
        /// </summary>
        [DataMember(Name = "config_value", EmitDefaultValue = false)]
        public string ConfigValue { get; set; }
    }
}
