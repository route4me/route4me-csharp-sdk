namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the avoidance zone(s) request
    /// </summary>
    public sealed class AvoidanceZoneQuery : GenericParameters
    {
        /// <summary>
        /// Unique ID of a device.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Unique ID of the territory.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "territory_id", EmitDefaultValue = false)]
        public string TerritoryId { get; set; }
    }
}
