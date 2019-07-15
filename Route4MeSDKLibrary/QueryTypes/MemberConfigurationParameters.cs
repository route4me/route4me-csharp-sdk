using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class MemberConfigurationParameters : GenericParameters
    {
        [DataMember(Name = "config_key", EmitDefaultValue = false)]
        public string config_key { get; set; }

        [DataMember(Name = "config_value", EmitDefaultValue = false)]
        public string config_value { get; set; }
    }
}