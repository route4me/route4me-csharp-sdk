using Route4MeSDK.DataTypes;
using System;
using System.Runtime.Serialization;
namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Route parameters accepted by endpoints.
    /// See also <seealso cref="GenericParameters"/>.
    /// </summary>
    [DataContract]
    public sealed class RouteParametersQuery : GenericParameters
    {
        /// <summary>
        /// Unique route ID.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        /// Pass True to return directions.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "directions", EmitDefaultValue = false)]
        public bool? Directions { get; set; }

        /// <summary>
        /// Whether return or not the path points in response.
        /// <para>Available values: <value>"None" - no path output. "Points" - points path output.</value></para>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "route_path_output", EmitDefaultValue = false)]
        public string RoutePathOutput { get; set; }

        /// <summary>
        /// Whether return or not the route tracking data in response.
        /// <para>Available values: </para>
        /// <value>0 - no device tracking history output. 1 - device tracking history output.</value>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "device_tracking_history", EmitDefaultValue = false)]
        public bool? DeviceTrackingHistory { get; set; }

        /// <summary>
        /// Limit the number of existing routes that should be returned per response when looking at a list of all the routes.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public uint? Limit { get; set; }

        /// <summary>
        /// Only the routes starting from that offset will be considered.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public uint? Offset { get; set; }

        /// <summary>
        /// Output addresses and directions in the original optimization request sequence. 
        /// This is to allow us to compare routes before & after optimization.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "original", EmitDefaultValue = false)]
        public bool? Original { get; set; }

        /// <summary>
        /// Output route and stop-specific notes. 
        /// The notes will have timestamps, note types, and geospatial information if available.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "notes", EmitDefaultValue = false)]
        public bool? Notes { get; set; }

        /// <summary>
        /// Search by contained text.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        /// Whether reoptimize or not the route.
        /// <remarks><para>
        /// Updating a route supports the reoptimize=1 parameter, which reoptimizes only that route. Also supports the parameters from GET.
        /// </para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "reoptimize", EmitDefaultValue = false)]
        public bool? ReOptimize { get; set; }

        /// <summary>
        /// Whether disable or not a route optimization.
        /// <para>Available values: </para>
        /// <value>true - disable a route optimization. false - not disable a route optimization.</value>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "disable_optimization", EmitDefaultValue = false)]
        public Nullable<bool> DisableOptimization { get; set; }

        /// <summary>
        /// The driving directions will be generated biased for this selection. This has no impact on route sequencing.
        /// <para>Available values: </para>
        /// <value>'Distance', 'Time', 'timeWithTraffic'.</value>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "optimize", EmitDefaultValue = false)]
        public string Optimize { get; set; }

        /// <summary>
        /// By sending recompute_directions=1 we request that the route directions be recomputed.
        /// <remarks><para>
        /// Note that this does happen automatically if certain properties of the route are updated, such as stop sequence_no changes or round-tripness.
        /// </para></remarks>
        /// <para>Available values: <value>1 - recompute directions, 0 - not recompute directions.</value></para> 
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "recompute_directions", EmitDefaultValue = false)]
        public int? RecomputeDirections { get; set; }

        /// <summary>
        /// Response format.
        /// <para>Available values: <value>json, xml.</value></para>
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "response_format", EmitDefaultValue = false)]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// Unique ID of a route destination.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public Nullable<int> RouteDestinationId { get; set; }

        /// <summary>
        /// If true, HTTP request will be redirected.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [IgnoreDataMember()]
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public Nullable<bool> Redirect { get; set; }

        /// <summary>
        /// Route Parameters to update. See <see cref="RouteParameters"/>
        /// <remarks><para>
        /// After a PUT there is no guarantee that the route_destination_id values are preserved! 
        /// It may create copies resulting in new destination IDs, especially when dealing with multiple depots.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters { get; set; }
    }
}
