using System.Runtime.Serialization;
namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Route parameters for the routes merging request.
    /// </summary>
    [DataContract]
    public sealed class MergeRoutesQuery : GenericParameters
    {
        /// <summary>
        /// Comma-delimited list of the routes' IDs to be merged.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "route_ids", EmitDefaultValue = false)]
        public string RouteIds { get; set; }

        /// <summary>
        /// Depot address.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "depot_address", EmitDefaultValue = false)]
        public string DepotAddress { get; set; }

        /// <summary>
        /// If true, the origin routes will be removed.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "remove_origin", EmitDefaultValue = false)]
        public bool RemoveOrigin { get; set; }

        /// <summary>
        /// Latitude of a depot.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "depot_lat", EmitDefaultValue = false)]
        public double DepotLat { get; set; }

        /// <summary>
        /// Longitude of a depot.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "depot_lng", EmitDefaultValue = false)]
        public double DepotLng { get; set; }
    }
}
