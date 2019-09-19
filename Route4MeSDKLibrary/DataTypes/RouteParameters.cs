using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Route parameters
    /// </summary>
    [DataContract]
    public sealed class RouteParameters
    {
        /// <summary>
        /// Let the R4M API know if this SDK request is coming 
        /// from a file upload within your environment (for analytics).
        /// </summary>
        [DataMember(Name = "is_upload", EmitDefaultValue = false)]
        public string IsUpload { get; set; }

        /// <summary>
        /// The tour type of this route. rt is short for round trip, 
        /// the optimization engine changes its behavior for round trip routes.
        /// </summary>
        [DataMember(Name = "rt", EmitDefaultValue = false)]
        public bool? RT { get; set; }

        /// <summary>
        /// By disabling optimization, the route optimization engine 
        /// will not resequence the stops in your
        /// </summary>
        [DataMember(Name = "disable_optimization", EmitDefaultValue = false)]
        public bool? DisableOptimization { get; set; }

        /// <summary>
        /// The name of this route. this route name will be accessible in the search API, 
        /// and also will be displayed on the mobile device of a user.
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        /// The route start date in UTC, unix timestamp seconds.
        /// Used to show users when the route will begin, also used for reporting and analytics.
        /// </summary>
        [DataMember(Name = "route_date", EmitDefaultValue = false)]
        public long? RouteDate { get; set; }

        /// <summary>
        /// Offset in seconds relative to the route start date (i.e. 9AM would be 60 * 60 * 9)
        /// </summary>
        [DataMember(Name = "route_time", EmitDefaultValue = false)]
        public object RouteTime { get; set; }

        /// <summary>
        /// Specify if the route can be viewed by unauthenticated users.
        /// </summary>
        [Obsolete("Always false")]
        [DataMember(Name = "shared_publicly", EmitDefaultValue = false)]
        public string SharedPublicly { get; set; }


        /// <summary>Gets or sets the optimize parameter.
        /// <para>Availabale values:</para>
        /// <value>Distance</value>, 
        /// <value>Time</value>, 
        /// <value>timeWithTraffic</value>
        /// </summary>
        [DataMember(Name = "optimize", EmitDefaultValue = false)]
        public string Optimize { get; set; }

        /// <summary>
        /// When the tour type is not round trip (rt = false), 
        /// enable lock last so that the final destination is fixed.
        /// <remarks>
        /// <para>
        /// Example: driver leaves a depot, but must always arrive at home 
        /// (or a specific gas station) at the end of the route.
        /// </para>
        /// </remarks>
        /// </summary>
        [DataMember(Name = "lock_last", EmitDefaultValue = false)]
        public bool? LockLast { get; set; }

        /// <summary>
        /// Vehicle capacity.
        /// <para>How much cargo can the vehicle carry (units, e.g. cubic meters)</para>
        /// </summary>
        [DataMember(Name = "vehicle_capacity", EmitDefaultValue = false)]
        public int? VehicleCapacity { get; set; }

        /// <summary>
        /// Maximum distance for a single vehicle in the route (always in miles)
        /// </summary>
        [DataMember(Name = "vehicle_max_distance_mi", EmitDefaultValue = false)]
        public int? VehicleMaxDistanceMI { get; set; }

        /// <summary>
        /// Maximum allowed revenue from a subtour
        /// </summary>
        [DataMember(Name = "subtour_max_revenue", EmitDefaultValue = false)]
        public int? SubtourMaxRevenue { get; set; }

        /// <summary>
        /// Maximum cargo volume a vehicle can cary
        /// </summary>
        [DataMember(Name = "vehicle_max_cargo_volume", EmitDefaultValue = false)]
        public double? VehicleMaxCargoVolume { get; set; }

        /// <summary>
        /// Maximum cargo weight a vehicle can cary
        /// </summary>
        [DataMember(Name = "vehicle_max_cargo_weight", EmitDefaultValue = false)]
        public double? VehicleMaxCargoWeight { get; set; }

        /// <summary>
        /// The distance measurement unit for the route.
        /// </summary>
        /// <remarks>km or mi, the route4me api will convert all measurements into these units</remarks>
        [DataMember(Name = "distance_unit", EmitDefaultValue = false)]
        public string DistanceUnit { get; set; }

        /// <summary>
        /// The mode of travel that the directions should be optimized for.
        /// <para>Available values:
        /// <value>Driving</value>, 
        /// <value>Walking</value>, 
        /// <value>Trucking</value>, 
        /// <value>Cycling</value>, 
        /// <value>Transit</value>.
        /// </para>
        /// </summary>
        [DataMember(Name = "travel_mode", EmitDefaultValue = false)]
        public string TravelMode { get; set; }

        /// <summary>
        /// Options which let the user choose which road obstacles to avoid. 
        /// This has no impact on route sequencing.
        /// <para>Available values:
        /// <value>Highways</value>, 
        /// <value>Tolls</value>, 
        /// <value>minimizeHighways</value>, 
        /// <value>minimizeTolls</value>, 
        /// <value>""</value>.
        /// </para>
        /// </summary>
        [DataMember(Name = "avoid", EmitDefaultValue = false)]
        public string Avoid { get; set; }

        /// <summary>
        /// An array of the Avoidance zones IDs
        /// </summary>
        [DataMember(Name = "avoidance_zones", EmitDefaultValue = false)]
        public string[] AvoidanceZones { get; set; }

        /// <summary>
        /// The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        /// The vehicle ID, to be assigned to the route.
        /// </summary>
        [Obsolete("All new routes should be assigned to a member_id")]
        [DataMember(Name = "driver_id", EmitDefaultValue = false)]
        public string DriverId { get; set; }

        /// <summary>
        /// The latitude of the device making this sdk request
        /// </summary>
        [DataMember(Name = "dev_lat", EmitDefaultValue = false)]
        public double? DevLatitude { get; set; }

        /// <summary>
        /// The longitude of the device making this sdk request
        /// </summary>
        [DataMember(Name = "dev_lng", EmitDefaultValue = false)]
        public double? DevLongitude { get; set; }

        /// <summary>
        /// <note type="note"><br />When using a multiple driver algorithm, this is the maximum permissible duration of a generated route.
        /// <para>The optimization system will automatically create more routes when the route_max_duration is exceeded for a route.</para>
        /// <para>However it will create an 'unrouted' list of addresses if the maximum number of drivers is exceeded</para>
        /// </note>
        /// </summary>
        /// <value>The maximum duration of the route.</value>
        [DataMember(Name = "route_max_duration", EmitDefaultValue = false)]
        public int? RouteMaxDuration { get; set; }

        /// <summary>The email address to notify upon completion of an optimization request</summary>
        /// <value>The route email.</value>
        [DataMember(Name = "route_email", EmitDefaultValue = false)]
        public string RouteEmail { get; set; }

        /// <summary>Type of route being created: ENUM(api,null)</summary>
        /// <value>The type of the route.</value>
        [DataMember(Name = "route_type", EmitDefaultValue = false)]
        public string RouteType { get; set; }

        [Obsolete("All routes are stored by default at this time")]
        [DataMember(Name = "store_route", EmitDefaultValue = false)]
        public bool? StoreRoute { get; set; }

        /// <summary>
        /// Metric system. Available values:
        /// <para><value>1 = ROUTE4ME_METRIC_EUCLIDEAN</value> (use euclidean distance when computing point to point distance)</para>
        /// <para><value>2 = ROUTE4ME_METRIC_MANHATTAN</value> (use manhattan distance (taxicab geometry) when computing point to point distance)</para>
        /// <para><value>3 = ROUTE4ME_METRIC_GEODESIC</value> (use geodesic distance when computing point to point distance)</para>
        /// <para><value>4 = ROUTE4ME_METRIC_MATRIX (default)</value> (use road network driving distance when computing point to point distance)</para>
        /// <para><value>5 = ROUTE4ME_METRIC_EXACT_2D</value> (use exact rectilinear distance)</para>
        /// </summary>
        [DataMember(Name = "metric", EmitDefaultValue = false)]
        public Metric Metric { get; set; }

        //the type of algorithm to use when optimizing the route

        /// <summary>
        /// The algorithm type to use when optimizing the route. See <see cref="DataTypes.AlgorithmType"/>
        /// </summary>
        [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
        public AlgorithmType AlgorithmType { get; set; }

        /// <summary>
        /// The route owner's member ID.
        /// <remarks>
        /// <para>In order for users in your organization to have routes assigned to them, 
        /// you must provide their member ID within the Route4Me system.</para>
        /// <para>A list of member IDs can be retrieved with view_users API method.</para>
        /// </remarks>
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }


        /// <summary>
        /// Specify the ip address of the remote user making this optimization request.
        /// </summary>
        [DataMember(Name = "ip", EmitDefaultValue = false)]
        public string Ip { get; set; }

        /// <summary>
        /// The method to use when compute the distance between the points in a route.
        /// <para>Available values:</para>
        /// <para><value>1 = DEFAULT</value> (R4M PROPRIETARY ROUTING)</para>
        /// <para><value>2 = DEPRECRATED</value></para>
        /// <para><value>3 = R4M TRAFFIC ENGINE</value></para>
        /// <para><value>4 = DEPRECATED</value></para>
        /// <para><value>5 = DEPRECATED</value></para>
        /// <para><value>6 = TRUCKING</value></para>
        /// </summary>
        [DataMember(Name = "dm", EmitDefaultValue = false)]
        public int? DM { get; set; }

        /// <summary>
        /// Directions method.
        /// <para>Available values:</para>
        /// <para><value>1 = DEFAULT</value> (R4M PROPRIETARY INTERNAL NAVIGATION SYSTEM)</para>
        /// <para><value>2 = DEPRECATED</value></para>
        /// <para><value>3 = TRUCKING</value></para>
        /// <para><value>4 = DEPRECATED</value></para>
        /// </summary>
        [DataMember(Name = "dirm", EmitDefaultValue = false)]
        public int? Dirm { get; set; }

        /// <summary>
        /// Legacy feature which permits a user to request an example number of optimized routes.
        /// </summary>
        [DataMember(Name = "parts", EmitDefaultValue = false)]
        public int? Parts { get; set; }

        /// <summary>
        /// Minimum number of optimized routes.
        /// </summary>
        [DataMember(Name = "parts_min", EmitDefaultValue = false)]
        public int? PartsMin { get; set; }

        /// <summary>
        /// 32 Character MD5 String ID of the device that was used to plan this route.
        /// </summary>
        [Obsolete("Always null")]
        [DataMember(Name = "device_id", EmitDefaultValue = false)]
        public object DeviceID { get; set; }

        /// <summary>
        /// The type of device making this request.
        /// <para>Available values:</para>
        /// <value>web</value>, 
        /// <value>iphone</value>, 
        /// <value>ipad</value>, 
        /// <value>android_phone</value>, 
        /// <value>android_tablet</value>
        /// </summary>
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        /// <summary>
        /// If true, the vehicle has a trailer.
        /// <remarks>
        /// <para>For routes that have trucking directions enabled, directions generated
        /// will ensure compliance so that road directions generated do not take the vehicle
        /// where trailers are prohibited.</para>
        /// </remarks>
        /// </summary>
        [DataMember(Name = "has_trailer", EmitDefaultValue = false)]
        public bool? HasTrailer { get; set; }

        /// <summary>
        /// If true, the vehicle will first drive then wait between stops.
        /// </summary>
        [DataMember(Name = "first_drive_then_wait_between_stops", EmitDefaultValue = false)]
        public bool? FirstDriveThenWaitBetweenStops { get; set; }

        /// <summary>
        /// The vehicle's trailer weight
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance so that road directions generated do not take the vehicle 
        /// on roads where the weight of the vehicle in tons exceeds this value.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "trailer_weight_t", EmitDefaultValue = false)]
        public double? TrailerWeightT { get; set; }

        /// <summary>
        /// If travel_mode is Trucking, specifies the truck weight.
        /// </summary>
        [DataMember(Name = "limited_weight_t", EmitDefaultValue = false)]
        public double? LimitedWeightT { get; set; }

        /// <summary>
        /// The vehicle's weight per axle (tons)
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated
        /// will ensure compliance so that road directions generated do not take the vehicle
        /// where the weight per axle in tons exceeds this value.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "weight_per_axle_t", EmitDefaultValue = false)]
        public double? WeightPerAxleT { get; set; }

        /// <summary>
        /// The truck height.
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance of this maximum height of truck when generating 
        /// road network driving directions.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "truck_height", EmitDefaultValue = false)]
        public double? TruckHeightMeters { get; set; }

        /// <summary>
        /// The truck width.
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance of this width of the truck when generating road network 
        /// driving directions.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "truck_width", EmitDefaultValue = false)]
        public double? TruckWidthMeters { get; set; }

        /// <summary>
        /// The truck length.
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance of this length of the truck when generating 
        /// road network driving directions.
        /// </para></remarks>
        /// </summary>
        [DataMember(Name = "truck_length", EmitDefaultValue = false)]
        public double? TruckLengthMeters { get; set; }

        /// <summary>
        /// The minimum number of stops permitted per created subroute.
        /// </summary>
        [DataMember(Name = "min_tour_size", EmitDefaultValue = false)]
        public int? MinTourSize { get; set; }

        /// <summary>
        /// The maximum number of stops permitted per created subroute.
        /// </summary>
        [DataMember(Name = "max_tour_size", EmitDefaultValue = false)]
        public int? MaxTourSize { get; set; }

        /// <summary>
        /// The optimization quality.
        /// <para>Available values:</para>
        /// <para><value>1</value> - Generate Optimized Routes As Quickly as Possible;</para>
        /// <para><value>2</value> - Generate Routes That Look Better On A Map;</para>
        /// <para><value>3</value> - Generate The Shortest And Quickest Possible Routes.</para>
        /// </summary>
        [DataMember(Name = "optimization_quality", EmitDefaultValue = false)]
        public int? OptimizationQuality { get; set; }

        /// <summary>
        /// If equal to 1, uturn is allowed for the vehicle.
        /// </summary>
        [DataMember(Name = "uturn", EmitDefaultValue = false)]
        public int? Uturn { get; set; }

        /// <summary>
        /// If equal to 1, leftturn is allowed for the vehicle.
        /// </summary>
        [DataMember(Name = "leftturn", EmitDefaultValue = false)]
        public int? LeftTurn { get; set; }

        /// <summary>
        /// If equal to 1, rightturn is allowed for the vehicle.
        /// </summary>
        [DataMember(Name = "rightturn", EmitDefaultValue = false)]
        public int? RightTurn { get; set; }

        /// <summary>
        /// If the service time is specified, all the route addresses wil have same service time. 
        /// See <see cref="OverrideAddresses"/>
        /// </summary>
        [DataMember(Name = "override_addresses", EmitDefaultValue = false)]
        public OverrideAddresses overrideAddresses { get; set; }

    }

    /// <summary>
    /// Subclass of the class RouteParameters. See <see cref="RouteParameters.overrideAddresses"/>
    /// </summary>
    public class OverrideAddresses
    {
        /// <summary>
        /// The service time specified or all the addresses in the route.
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false), CustomValidation(typeof(PropertyValidation), "ValidateEpochTime")]
        public int? Time { get; set; }
    }


}
