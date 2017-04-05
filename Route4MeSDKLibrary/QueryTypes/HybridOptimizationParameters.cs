using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{

    [DataContract]
    public sealed class HybridOptimizationParameters : GenericParameters
    {
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "target_date_string", EmitDefaultValue = false)]
        public string target_date_string { get; set; }

        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "timezone_offset_minutes", EmitDefaultValue = false)]
        public int timezone_offset_minutes { get; set; }

    }
}