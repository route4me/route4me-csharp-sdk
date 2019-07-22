using System;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the territory(ies) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class TerritoryQuery : GenericParameters
    {
        /// <summary>
        /// Device Id
        /// </summary>
        [Obsolete("This parameter is not used in the territory request.")]
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceID { get; set; }


        /// <summary>
        /// Unique ID of a territory.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "territory_id", EmitDefaultValue = false)]
        public string TerritoryId { get; set; }


        /// <summary>
        /// If equal to 1, the addresses inside the territory will be returned.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public Nullable<int> Addresses { get; set; }
    }
}
