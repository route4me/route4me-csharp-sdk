
using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;
namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Route parameters accepted by endpoints
    /// </summary>
    [DataContract]
    public sealed class RouteParametersQuery : GenericParameters
    {
        /// <summary>
        /// Route Identifier
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///  	Pass True to return directions and the route path
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "directions", EmitDefaultValue = false)]
        public bool? Directions { get; set; }

        /// <summary>
        /// "None" - no path output. "Points" - points path output
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "route_path_output", EmitDefaultValue = false)]
        public string RoutePathOutput { get; set; }

        /// <summary>
        /// Output route tracking data in response
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "device_tracking_history", EmitDefaultValue = false)]
        public bool? DeviceTrackingHistory { get; set; }

        /// <summary>
        /// The number of existing routes that should be returned per response when looking at a list of all the routes.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// The page number for route listing pagination. Increment the offset by the limit number to move to the next page.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "start_date", EmitDefaultValue = false)]
        public string StartDate { get; set; }

        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "end_date", EmitDefaultValue = false)]
        public string EndDate { get; set; }

        /// <summary>
        /// Output addresses and directions in the original optimization request sequence. This is to allow us to compare routes before & after optimization.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "original", EmitDefaultValue = false)]
        public bool? Original { get; set; }

        /// <summary>
        /// Output route and stop-specific notes. The notes will have timestamps, note types, and geospatial information if available
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "notes", EmitDefaultValue = false)]
        public bool? Notes { get; set; }

        /// <summary>
        /// Search query
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        /// Updating a route supports the reoptimize=1 parameter, which reoptimizes only that route. Also supports the parameters from GET.
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "reoptimize", EmitDefaultValue = false)]
        public bool? ReOptimize { get; set; }


        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "disable_optimization", EmitDefaultValue = false)]
        public bool? DisableOptimization { get; set; }

        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "optimize", EmitDefaultValue = false)]
        public string Optimize { get; set; }

        /// <summary>
        /// By sending recompute_directions=1 we request that the route directions be recomputed (note that this does happen automatically if certain properties of the route are updated, such as stop sequence_no changes or round-tripness)
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "recompute_directions", EmitDefaultValue = false)]
        public bool? RecomputeDirections { get; set; }

        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "response_format", EmitDefaultValue = false)]
        public string ResponseFormat { get; set; }

        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public int? RouteDestinationId { get; set; }

        /// <summary>
        /// If true will be redirected
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public bool? Redirect { get; set; }

        /// <summary>
        /// Route Parameters to update.
        /// (After a PUT there is no guarantee that the route_destination_id values are preserved! It may create copies resulting in new destination IDs, especially when dealing with multiple depots.)
        /// </summary>
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters { get; set; }

        /// <summary>
        /// Array of the route addresses
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses { get; set; }

        /// <summary>
        /// If true, the route is approved for execution.
        /// </summary>
        [DataMember(Name = "approved_for_execution", EmitDefaultValue = false)]
        public bool ApprovedForExecution { get; set; }
    }
}
