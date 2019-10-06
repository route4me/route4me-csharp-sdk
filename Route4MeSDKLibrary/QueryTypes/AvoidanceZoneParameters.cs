using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the Avoidance zone(s) requst.
    /// </summary>
    [DataContract]
    public sealed class AvoidanceZoneParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of a device.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Unique ID of the territory.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "territory_id", EmitDefaultValue = false)]
        public string TerritoryId { get; set; }

        /// <summary>
        /// Territory name.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "territory_name")]
        public string TerritoryName { get; set; }

        /// <summary>
        /// Territory color.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "territory_color")]
        public string TerritoryColor { get; set; }

        /// <summary>
        /// Member Id.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "member_id")]
        public string MemberId { get; set; }

        /// <summary>
        /// Territory parameters. See <see cref="DataTypes.Territory"/>.
        /// <remarks><para>Data member parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "territory")]
        public Territory Territory { get; set; }
    }
}
