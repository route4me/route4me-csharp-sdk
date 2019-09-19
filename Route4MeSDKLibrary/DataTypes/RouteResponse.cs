using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from a route request.
    /// </summary>
    [DataContract]
    public sealed class RouteResponse
    {
        /// <summary>
        /// The route ID.
        /// </summary>
        [DataMember(Name = "route_id")]
        public string RouteID { get; set; }

        /// <summary>
        /// The optimization problem ID.
        /// </summary>
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }


        /// <summary>
        /// Route rating by user [0 - 5].
        /// </summary>
        [DataMember(Name = "user_route_rating")]
        public Nullable<int> UserRouteRating { get; set; }

        /// <summary>
        /// Route owner's member ID.
        /// </summary>
        [DataMember(Name = "member_id")]
        public Nullable<int> MemberId { get; set; }

        /// <summary>
        /// Route owner's email.
        /// </summary>
        [DataMember(Name = "member_email")]
        public string MemberEmail { get; set; }

        /// <summary>
        /// Route owner's first name.
        /// </summary>
        [DataMember(Name = "member_first_name")]
        public string MemberFirstName { get; set; }

        /// <summary>
        /// Route owner's last name.
        /// </summary>
        [DataMember(Name = "member_last_name")]
        public string MemberLastName { get; set; }

        /// <summary>
        /// Channel name.
        /// </summary>
        [DataMember(Name = "channel_name")]
        public string ChannelName { get; set; }

        /// <summary>
        /// Vehicle alias.
        /// </summary>
        [DataMember(Name = "vehicle_alias")]
        public string VehicleAlias { get; set; }

        /// <summary>
        /// Driver alias.
        /// </summary>
        [DataMember(Name = "driver_alias")]
        public string DriverAlias { get; set; }

        /// <summary>
        /// The total distance of the route trip.
        /// </summary>
        [DataMember(Name = "trip_distance")]
        public Nullable<double> TripDistance { get; set; }

        /// <summary>
        /// If true, route is unrouted.
        /// </summary>
        [DataMember(Name = "is_unrouted")]
        public bool IsUnrouted { get; set; }

        /// <summary>
        /// Total cost of a route.
        /// </summary>
        [DataMember(Name = "route_cost")]
        public Nullable<double> RouteCost { get; set; }

        /// <summary>
        /// Total revenue of a route.
        /// </summary>
        [DataMember(Name = "route_revenue")]
        public Nullable<double> RouteRevenue { get; set; }

        /// <summary>
        /// Net revenue per distance unit.
        /// </summary>
        [DataMember(Name = "net_revenue_per_distance_unit")]
        public Nullable<double> NetRevenuePerDistanceUnit { get; set; }

        /// <summary>
        /// When the route was created.
        /// </summary>
        [DataMember(Name = "created_timestamp")]
        public Nullable<int> CreatedTimestamp { get; set; }

        /// <summary>
        /// Miles per gallon.
        /// </summary>
        [DataMember(Name = "mpg")]
        public Nullable<double> Mpg { get; set; }

        /// <summary>
        /// Gas price.
        /// </summary>
        [DataMember(Name = "gas_price")]
        public Nullable<double> GasPrice { get; set; }

        /// <summary>
        /// Total route duration (seconds).
        /// <remarks><para>
        /// Sum of the trip durations to the next addresses.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "route_duration_sec")]
        public Nullable<int> RouteDurationSec { get; set; }

        /// <summary>
        /// Planned total route duration.
        /// <remarks><para>
        /// The duration between the latest window end and the earliest window start.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "planned_total_route_duration")]
        public Nullable<int> PlannedTotalRouteDuration { get; set; }

        /// <summary>
        /// Actual travel distance.
        /// </summary>
        [DataMember(Name = "actual_travel_distance")]
        public Nullable<double> ActualTravelDistance { get; set; }

        /// <summary>
        /// Actual travel time.
        /// </summary>
        [DataMember(Name = "actual_travel_time")]
        public Nullable<int> ActualTravelTime { get; set; }

        /// <summary>
        /// Actual footsteps.
        /// </summary>
        [DataMember(Name = "actual_footsteps")]
        public Nullable<int> ActualFootSteps { get; set; }

        /// <summary>
        /// Working time
        /// </summary>
        [DataMember(Name = "working_time")]
        public Nullable<int> WorkingTime { get; set; }

        /// <summary>
        /// Driving time
        /// </summary>
        [DataMember(Name = "driving_time")]
        public Nullable<int> DrivingTime { get; set; }

        /// <summary>
        /// Idling time
        /// </summary>
        [DataMember(Name = "idling_time")]
        public Nullable<int> IdlingTime { get; set; }

        /// <summary>
        /// Paying miles
        /// </summary>
        [DataMember(Name = "paying_miles")]
        public Nullable<double> PayingMiles { get; set; }

        /// <summary>
        /// Geofence polygon type.
        /// </summary>
        [DataMember(Name = "geofence_polygon_type")]
        public string GeofencePolygonType { get; set; }

        /// <summary>
        /// Geofence polygon size.
        /// </summary>
        [DataMember(Name = "geofence_polygon_size")]
        public Nullable<int> GeofencePolygonSize { get; set; }

        /// <summary>
        /// Destination count.
        /// </summary>
        [DataMember(Name = "destination_count")]
        public int? DestinationCount { get; set; }

        /// <summary>
        /// Notes count in the route.
        /// </summary>
        [DataMember(Name = "notes_count")]
        public int? NotesCount { get; set; }

        /// <summary>
        /// The route parameters. See <see cref="RouteParameters"/>
        /// </summary>
        [DataMember(Name = "parameters")]
        public RouteParameters Parameters { get; set; }

        /// <summary>
        /// An array of the Address type objects. 
        /// See <see cref="Address"/>
        /// </summary>
        [DataMember(Name = "addresses")]
        public Address[] Addresses { get; set; }

        /// <summary>
        /// Links to the generated routes. See <see cref="DataTypes.Links"/>
        /// </summary>
        [DataMember(Name = "links")]
        public Links Links { get; set; }

        /// <summary>
        /// Member config key-value pairs.
        /// </summary>
        [DataMember(Name = "member_config_storage")]
        public Dictionary<String, String> MemberConfigStorage { get; set; }

        /// <summary>
        /// An array of the AddressNote type objects. 
        /// See <see cref="DataTypes.AddressNote"/>
        /// </summary>
        [DataMember(Name = "notes")]
        public AddressNote[] Notes { get; set; }

        /// <summary>
        /// Edge-wise path to be drawn on the map. 
        /// See <see cref="DataTypes.GeoPoint"/>
        /// </summary>
        [DataMember(Name = "path")]
        public GeoPoint[] Path { get; set; }

        /// <summary>
        /// Edge by edge turn-by-turn directions. 
        /// See <see cref="DataTypes.Direction"/>
        /// </summary>
        [DataMember(Name = "directions")]
        public Direction[] Directions { get; set; }
    }
}
