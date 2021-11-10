using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The Avoidance Zone class.
    /// </summary>
    [DataContract]
    public sealed class AvoidanceZone
    {
        /// <summary>
        ///     Avoidance zone ID
        /// </summary>
        [DataMember(Name = "territory_id")]
        public string TerritoryId { get; set; }

        /// <summary>
        ///     Territory name
        /// </summary>
        [DataMember(Name = "territory_name")]
        public string TerritoryName { get; set; }

        /// <summary>
        ///     Territory color
        /// </summary>
        [DataMember(Name = "territory_color")]
        public string TerritoryColor { get; set; }

        /// <summary>
        ///     Member ID
        /// </summary>
        [DataMember(Name = "member_id")]
        public string MemberId { get; set; }

        /// <summary>
        ///     The territory parameters specifying the territory shape.
        /// </summary>
        [DataMember(Name = "territory")]
        public Territory Territory { get; set; }
    }
}