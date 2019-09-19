
namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the route address(es) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class AddressParameters : GenericParameters
    {
        /// <summary>
        /// Unique ID of a route.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        /// Unique ID of a route destination.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public int RouteDestinationId { get; set; }

        /// <summary>
        /// Unique ID of a route destination.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public int AddressId { get; set; }

        /// <summary>
        /// If true, the route destination notes will be included in a response.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "notes")]
        public bool Notes { get; set; }

        /// <summary>
        /// If true, the route destination will be marked as departed.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_departed")]
        public bool IsDeparted { get; set; }

        /// <summary>
        /// If true, the route destination will be marked as visited.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "is_visited")]
        public bool IsVisited { get; set; }

    }
}
