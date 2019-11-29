using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Activity Type Class
    /// </summary>
    [DataContract]
    public sealed class Activity : GenericParameters
    {
        /// <summary>
        /// Activity ID
        /// </summary>
        [DataMember(Name = "activity_id", EmitDefaultValue = false)]
        public string ActivityId { get; set; }

        /// <summary>
        /// Activity Type. See <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Activity.dtd#L23"/> 
        /// </summary>
        [DataMember(Name = "activity_type", EmitDefaultValue = false)]
        public string ActivityType { get; set; }

        /// <summary>
        /// Activity timestamp - The time when the activity occurred.
        /// </summary>
        [DataMember(Name = "activity_timestamp", EmitDefaultValue = false)]
        public long? ActivityTimestamp { get; set; }

        /// <summary>
        /// Activity message
        /// </summary>
        [DataMember(Name = "activity_message", EmitDefaultValue = false)]
        public string ActivityMessage { get; set; }

        /// <summary>
        /// Member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        /// Route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        /// Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public string RouteDestinationId { get; set; }
    }
}
