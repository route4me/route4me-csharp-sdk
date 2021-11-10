using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The route data structure
    /// </summary>
    [DataContract]
    public sealed class DataObjectRoute : DataObjectBase
    {
        /// <summary>
        ///     The route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     User route rating [0, 5]. A null value means no rating was given.
        ///     Users can rate routes so that future optimizations take these ratings into account.
        /// </summary>
        [DataMember(Name = "user_route_rating", EmitDefaultValue = false)]
        public int? UserRouteRating { get; set; }

        /// <summary>
        ///     The member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     The member's email
        /// </summary>
        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string MemberEmail { get; set; }

        /// <summary>
        ///     The member's first name
        /// </summary>
        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string MemberFirstName { get; set; }

        /// <summary>
        ///     The member's last name
        /// </summary>
        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string MemberLastName { get; set; }

        /// <summary>
        ///     Channel name.
        /// </summary>
        [DataMember(Name = "channel_name", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string ChannelName { get; set; }

        /// <summary>
        ///     URL to a member picture
        /// </summary>
        [DataMember(Name = "member_picture", EmitDefaultValue = false)]
        public string MemberPicture { get; set; }

        /// <summary>
        ///     Member tracking subheadline
        /// </summary>
        [DataMember(Name = "member_tracking_subheadline", EmitDefaultValue = false)]
        public string MemberTrackingSubheadline { get; set; }

        /// <summary>
        ///     If true, the order is approved for execution
        /// </summary>
        [DataMember(Name = "approved_for_execution")]
        public bool ApprovedForExecution { get; set; }

        /// <summary>
        ///     Counter of the approved revisions
        /// </summary>
        [DataMember(Name = "approved_revisions_counter", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? ApprovedRevisionsCounter { get; set; }

        /// <summary>
        ///     Vehicle alias
        /// </summary>
        [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
        public string VehicleAlias { get; set; }

        /// <summary>
        ///     Driver alias
        /// </summary>
        [DataMember(Name = "driver_alias", EmitDefaultValue = false)]
        public string DriverAlias { get; set; }

        /// <summary>
        ///     Cost of the route
        /// </summary>
        [DataMember(Name = "route_cost", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? RouteCost { get; set; }

        /// <summary>
        ///     Route weight
        /// </summary>
        [DataMember(Name = "route_weight", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? RouteWeight { get; set; }

        /// <summary>
        ///     Route total cubic volume of the carried cargo (vehicle capacity)
        /// </summary>
        [DataMember(Name = "route_cube", EmitDefaultValue = false)]
        public double? RouteCube { get; set; }

        /// <summary>
        ///     The total number of route pieces (vehicle pieces).
        /// </summary>
        [DataMember(Name = "route_pieces", EmitDefaultValue = false)]
        public int? RoutePieces { get; set; }

        /// <summary>
        ///     Total route revenue
        /// </summary>
        [DataMember(Name = "route_revenue", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? RouteRevenue { get; set; }

        /// <summary>
        ///     Net revenue per distance unit
        /// </summary>
        [DataMember(Name = "net_revenue_per_distance_unit", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? NetRevenuePerDistanceUnit { get; set; }

        /// <summary>
        ///     Miles per gallon
        /// </summary>
        [DataMember(Name = "mpg", EmitDefaultValue = false)]
        public string mpg { get; set; }

        /// <summary>
        ///     Total route's trip distance
        /// </summary>
        [DataMember(Name = "trip_distance", EmitDefaultValue = false)]
        public double? TripDistance { get; set; }

        /// <summary>
        ///     The UDU distance measurement unit for the route.
        ///     enum: ["mi", "km"]
        /// </summary>
        /// <remarks>km or mi, the route4me api will convert all measurements into these units</remarks>
        [DataMember(Name = "udu_distance_unit", EmitDefaultValue = false)]
        public string UduDistanceUnit { get; set; }

        /// <summary>
        ///     Total route's UDU trip distance
        /// </summary>
        [DataMember(Name = "udu_trip_distance", EmitDefaultValue = false)]
        public double? UduTripDistance { get; set; }

        /// <summary>
        ///     If true, route is unrouted.
        /// </summary>
        [DataMember(Name = "is_unrouted", EmitDefaultValue = false)]
        public bool? IsUnrouted { get; set; }

        /// <summary>
        ///     Gas price
        /// </summary>
        [DataMember(Name = "gas_price", EmitDefaultValue = false)]
        public double? GasPrice { get; set; }

        /// <summary>
        ///     Total route duration (seconds)
        /// </summary>
        [DataMember(Name = "route_duration_sec", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? RouteDurationSec { get; set; }

        /// <summary>
        ///     Planned total route duration (seconds).
        /// </summary>
        [DataMember(Name = "planned_total_route_duration", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? PlannedTotalRouteDuration { get; set; }

        /// <summary>
        ///     Total wait time (seconds).
        /// </summary>
        [DataMember(Name = "total_wait_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? TotalWaitTime { get; set; }

        /// <summary>
        ///     UDU Actual travel distance.
        /// </summary>
        [DataMember(Name = "udu_actual_travel_distance", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? UduActualTravelDistance { get; set; }

        /// <summary>
        ///     Actual travel distance.
        /// </summary>
        [DataMember(Name = "actual_travel_distance", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? ActualTravelDistance { get; set; }

        /// <summary>
        ///     Actual travel time (seconds).
        /// </summary>
        [DataMember(Name = "actual_travel_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? ActualTravelTime { get; set; }

        /// <summary>
        ///     Actual footsteps.
        /// </summary>
        [DataMember(Name = "actual_footsteps", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? ActualFootsteps { get; set; }

        /// <summary>
        ///     Working time.
        /// </summary>
        [DataMember(Name = "working_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? WorkingTime { get; set; }

        /// <summary>
        ///     Driving time.
        /// </summary>
        [DataMember(Name = "driving_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? DrivingTime { get; set; }

        /// <summary>
        ///     Idling time
        /// </summary>
        [DataMember(Name = "idling_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? IdlingTime { get; set; }

        /// <summary>
        ///     Paying miles
        /// </summary>
        [DataMember(Name = "paying_miles", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? PayingMiles { get; set; }

        /// <summary>
        ///     Geofence polygon type.
        ///     enum: ["circle", "poly", "rect"]
        /// </summary>
        [DataMember(Name = "geofence_polygon_type", EmitDefaultValue = false)]
        public string GeofencePolygonType { get; set; }

        /// <summary>
        ///     Geofence polygon size.
        /// </summary>
        [DataMember(Name = "geofence_polygon_size", EmitDefaultValue = false)]
        public int? GeofencePolygonSize { get; set; }

        /// <summary>
        ///     Destination count.
        /// </summary>
        [DataMember(Name = "destination_count", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? DestinationCount { get; set; }

        /// <summary>
        ///     Notes count in the route.
        /// </summary>
        [DataMember(Name = "notes_count", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? NotesCount { get; set; }

        /// <summary>
        ///     Route notes
        /// </summary>
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public AddressNote[] Notes { get; set; }

        /// <summary>
        ///     Edge by edge turn-by-turn directions. See <see cref="Direction" />
        /// </summary>
        [DataMember(Name = "directions")]
        [ReadOnly(true)]
        public Direction[] Directions { get; set; }

        /// <summary>
        ///     Edge-wise path to be drawn on the map See <see cref="DirectionPathPoint" />
        /// </summary>
        [DataMember(Name = "path")]
        [ReadOnly(true)]
        public DirectionPathPoint[] Path { get; set; }

        /// <summary>
        ///     A collection of device tracking data with coordinates, speed, and timestamps.
        ///     <para>See <see cref="TrackingHistory" /></para>
        /// </summary>
        [DataMember(Name = "tracking_history")]
        [ReadOnly(true)]
        public TrackingHistory[] TrackingHistory { get; set; }

        /// <summary>
        ///     A vehicle assigned to the route.
        /// </summary>
        [DataMember(Name = "vehicle", EmitDefaultValue = false)]
        public VehicleV4Response Vehilce { get; set; }

        /// <summary>
        ///     Member config key-value pairs.
        /// </summary>
        [DataMember(Name = "member_config_storage", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public Dictionary<string, string> MemberConfigStorage { get; set; }

        /// <summary>
        ///     Original route.
        /// </summary>
        [DataMember(Name = "original_route", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public DataObjectRoute OriginalRoute { get; set; }

        /// <summary>
        ///     Bundled items
        /// </summary>
        [DataMember(Name = "bundle_items", EmitDefaultValue = true)]
        [ReadOnly(true)]
        public BundledItemResponse[] BundleItems { get; set; }
    }
}