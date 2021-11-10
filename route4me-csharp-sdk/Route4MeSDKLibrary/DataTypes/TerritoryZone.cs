using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Territory zone data structure.
    /// </summary>
    [DataContract]
    public sealed class TerritoryZone
    {
        /// <summary>
        ///     Territory ID.
        /// </summary>
        [DataMember(Name = "territory_id")]
        public string TerritoryId { get; set; }

        /// <summary>
        ///     Territory name.
        /// </summary>
        [DataMember(Name = "territory_name")]
        public string TerritoryName { get; set; }

        /// <summary>
        ///     Territory color.
        /// </summary>
        [DataMember(Name = "territory_color")]
        public string TerritoryColor { get; set; }

        /// <summary>
        ///     The locations comprised in the territory.
        /// </summary>
        [DataMember(Name = "addresses")]
        public int[] Addresses { get; set; }

        /// <summary>
        ///     The orders comprised in the territory.
        /// </summary>
        [DataMember(Name = "orders")]
        public int[] Orders { get; set; }

        /// <summary>
        ///     Member Id.
        /// </summary>
        [DataMember(Name = "member_id")]
        public string MemberId { get; set; }

        /// <summary>
        ///     Territory parameters. See <see cref="DataTypes.Territory" />.
        /// </summary>
        [DataMember(Name = "territory")]
        public Territory Territory { get; set; }
    }
}