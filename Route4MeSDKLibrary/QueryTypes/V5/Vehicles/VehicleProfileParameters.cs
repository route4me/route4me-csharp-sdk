namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// Request parameters for the vehicle profiles.
    /// </summary>
    public sealed class VehicleProfileParameters : GenericParameters
    {
        /// <summary>
        /// If true, returned vehicle profile is paginated.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "with_pagination", EmitDefaultValue = false)]
        public bool WithPagination { get; set; }

        /// <summary>
        /// Current page number in the vehicles collection
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        /// Returned vehicles number per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "perPage", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

        /// <summary>
        /// Vehicle profile ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "id", EmitDefaultValue = false)]
        public int? VehicleProfileId { get; set; }

    }
}
