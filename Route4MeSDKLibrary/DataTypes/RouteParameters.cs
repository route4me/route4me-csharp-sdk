using System.Runtime.Serialization;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Route4MeSDK.DataTypes
{

    [DataContract]
    public sealed class RouteParameters
    {

        //let the R4M api know if this sdk request is coming from a file upload within your environment (for analytics)
        [DataMember(Name = "is_upload", EmitDefaultValue = false)]
        public string IsUpload { get; set; }

        //the tour type of this route. rt is short for round trip, the optimization engine changes its behavior for round trip routes
        [DataMember(Name = "rt", EmitDefaultValue = false)]
        public bool? RT { get; set; }

        //by disabling optimization, the route optimization engine will not resequence the stops in your
        [DataMember(Name = "disable_optimization", EmitDefaultValue = false)]
        public bool? DisableOptimization { get; set; }

        //the name of this route. this route name will be accessible in the search API, and also will be displayed on the mobile device of a user
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        //the route start date in UTC, unix timestamp seconds. 
        //used to show users when the route will begin, also used for reporting and analytics
        [DataMember(Name = "route_date", EmitDefaultValue = false)]
        public long? RouteDate { get; set; }

        //offset in seconds relative to the route start date (i.e. 9AM would be 60 * 60 * 9)
        [DataMember(Name = "route_time", EmitDefaultValue = false)]
        public object RouteTime { get; set; }

        //deprecated
        //specify if the route can be viewed by unauthenticated users
        [DataMember(Name = "shared_publicly", EmitDefaultValue = false)]
        public string SharedPublicly { get; set; }


        [DataMember(Name = "optimize", EmitDefaultValue = false)]
        public string Optimize { get; set; }

        //when the tour type is not round trip (rt = false), enable lock last so that the final destination is fixed
        //example: driver leaves a depot, but must always arrive at home ( or a specific gas station) at the end of the route
        [DataMember(Name = "lock_last", EmitDefaultValue = false)]
        public bool? LockLast { get; set; }


        [DataMember(Name = "vehicle_capacity", EmitDefaultValue = false)]
        public int? VehicleCapacity { get; set; }

        [DataMember(Name = "vehicle_max_distance_mi", EmitDefaultValue = false)]
        public int? VehicleMaxDistanceMI { get; set; }

        [DataMember(Name = "subtour_max_revenue", EmitDefaultValue = false)]
        public int? SubtourMaxRevenue { get; set; }

        [DataMember(Name = "vehicle_max_cargo_volume", EmitDefaultValue = false)]
        public double? VehicleMaxCargoVolume { get; set; }

        [DataMember(Name = "vehicle_max_cargo_weight", EmitDefaultValue = false)]
        public double? VehicleMaxCargoWeight { get; set; }

        //km or mi, the route4me api will convert all measurements into these units
        [DataMember(Name = "distance_unit", EmitDefaultValue = false)]
        public string DistanceUnit { get; set; }

        [DataMember(Name = "travel_mode", EmitDefaultValue = false)]
        public string TravelMode { get; set; }

        [DataMember(Name = "avoid", EmitDefaultValue = false)]
        public string Avoid { get; set; }

        [DataMember(Name = "avoidance_zones", EmitDefaultValue = false)]
        public string[] AvoidanceZones { get; set; }

        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        //deprecated, all new routes should be assigned to a member_id
        [DataMember(Name = "driver_id", EmitDefaultValue = false)]
        public string DriverId { get; set; }

        //the latitude of the device making this sdk request
        [DataMember(Name = "dev_lat", EmitDefaultValue = false)]
        public double? DevLatitude { get; set; }

        //the longitude of the device making this sdk request
        [DataMember(Name = "dev_lng", EmitDefaultValue = false)]
        public double? DevLongitude { get; set; }

        //when using a multiple driver algorithm, this is the maximum permissible duration of a generated route
        //the optimization system will automatically create more routes when the route_max_duration is exceeded for a route
        //however it will create an 'unrouted' list of addresses if the maximum number of drivers is exceeded
        [DataMember(Name = "route_max_duration", EmitDefaultValue = false)]
        public int? RouteMaxDuration { get; set; }

        //the email address to notify upon completion of an optimization request
        [DataMember(Name = "route_email", EmitDefaultValue = false)]
        public string RouteEmail { get; set; }

        //type of route being created: ENUM(api,null)
        [DataMember(Name = "route_type", EmitDefaultValue = false)]
        public string RouteType { get; set; }

        //deprecated
        //all routes are stored by default at this time
        [DataMember(Name = "store_route", EmitDefaultValue = false)]
        public bool? StoreRoute { get; set; }

        //1 = ROUTE4ME_METRIC_EUCLIDEAN (use euclidean distance when computing point to point distance)
        //2 = ROUTE4ME_METRIC_MANHATTAN (use manhattan distance (taxicab geometry) when computing point to point distance)
        //3 = ROUTE4ME_METRIC_GEODESIC (use geodesic distance when computing point to point distance)
        //#4 is the default and suggested metric
        //4 = ROUTE4ME_METRIC_MATRIX (use road network driving distance when computing point to point distance)
        //5 = ROUTE4ME_METRIC_EXACT_2D (use exact rectilinear distance)
        [DataMember(Name = "metric", EmitDefaultValue = false)]
        public Metric Metric { get; set; }

        //the type of algorithm to use when optimizing the route
        [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
        public AlgorithmType AlgorithmType { get; set; }

        //in order for users in your organization to have routes assigned to them, 
        //you must provide their member id within the route4me system
        //a list of member ids can be retrieved with view_users api method
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }


        //specify the ip address of the remote user making this optimization request
        [DataMember(Name = "ip", EmitDefaultValue = false)]
        public string Ip { get; set; }


        //the method to use when compute the distance between the points in a route
        //1 = DEFAULT (R4M PROPRIETARY ROUTING)
        //2 = DEPRECRATED
        //3 = R4M TRAFFIC ENGINE
        //4 = DEPRECATED
        //5 = DEPRECATED
        //6 = TRUCKING
        [DataMember(Name = "dm", EmitDefaultValue = false)]
        public int? DM { get; set; }

        //directions method
        //1 = DEFAULT (R4M PROPRIETARY INTERNAL NAVIGATION SYSTEM)
        //2 = DEPRECATED
        //3 = TRUCKING
        //4 = DEPRECATED
        [DataMember(Name = "dirm", EmitDefaultValue = false)]
        public int? Dirm { get; set; }

        [DataMember(Name = "parts", EmitDefaultValue = false)]
        public int? Parts { get; set; }

        [DataMember(Name = "parts_min", EmitDefaultValue = false)]
        public int? PartsMin { get; set; }

        //deprecated 
        [DataMember(Name = "device_id", EmitDefaultValue = false)]
        public object DeviceID { get; set; }

        //the type of device making this request
        //ENUM("web", "iphone", "ipad", "android_phone", "android_tablet")
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        //for routes that have trucking directions enabled, directions generated
        //will ensure compliance so that road directions generated do not take the vehicle
        //where trailers are prohibited
        [DataMember(Name = "has_trailer", EmitDefaultValue = false)]
        public bool? HasTrailer { get; set; }

        [DataMember(Name = "first_drive_then_wait_between_stops", EmitDefaultValue = false)]
        public bool? FirstDriveThenWaitBetweenStops { get; set; }

        //for routes that have trucking directions enabled, directions generated
        //will ensure compliance so that road directions generated do not take the vehicle
        //on roads where the weight of the vehicle in tons exceeds this value
        [DataMember(Name = "trailer_weight_t", EmitDefaultValue = false)]
        public double? TrailerWeightT { get; set; }


        [DataMember(Name = "limited_weight_t", EmitDefaultValue = false)]
        public double? LimitedWeightT { get; set; }

        //for routes that have trucking directions enabled, directions generated
        //will ensure compliance so that road directions generated do not take the vehicle
        //where the weight per axle in tons exceeds this value
        [DataMember(Name = "weight_per_axle_t", EmitDefaultValue = false)]
        public double? WeightPerAxleT { get; set; }

        //for routes that have trucking directions enabled, directions generated
        //will ensure compliance of this maximum height of truck when generating road network driving directions
        [DataMember(Name = "truck_height", EmitDefaultValue = false)]
        public double? TruckHeightMeters { get; set; }

        //for routes that have trucking directions enabled, directions generated
        //will ensure compliance of this width of the truck when generating road network driving directions
        [DataMember(Name = "truck_width", EmitDefaultValue = false)]
        public double? TruckWidthMeters { get; set; }

        //for routes that have trucking directions enabled, directions generated
        //will ensure compliance of this length of the truck when generating road network driving directions
        [DataMember(Name = "truck_length", EmitDefaultValue = false)]
        public double? TruckLengthMeters { get; set; }


        //the minimum number of stops permitted per created subroute
        [DataMember(Name = "min_tour_size", EmitDefaultValue = false)]
        public int? MinTourSize { get; set; }

        //the maximum number of stops permitted per created subroute
        [DataMember(Name = "max_tour_size", EmitDefaultValue = false)]
        public int? MaxTourSize { get; set; }

        //there are 3 types of optimization qualities that are optimizations goals
        //1 - Generate Optimized Routes As Quickly as Possible
        //2 - Generate Routes That Look Better On A Map
        //3 - Generate The Shortest And Quickest Possible Routes

        [DataMember(Name = "optimization_quality", EmitDefaultValue = false)]
        public int? OptimizationQuality { get; set; }

        [DataMember(Name = "uturn", EmitDefaultValue = false)]
        public int? Uturn { get; set; }

        [DataMember(Name = "leftturn", EmitDefaultValue = false)]
        public int? LeftTurn { get; set; }

        [DataMember(Name = "rightturn", EmitDefaultValue = false)]
        public int? RightTurn { get; set; }

        [DataMember(Name = "override_addresses", EmitDefaultValue = false)]
        public OverrideAddresses overrideAddresses { get; set; }

    }

    public class OverrideAddresses
    {
        [DataMember(Name = "time", EmitDefaultValue = false),  CustomValidation(typeof(PropertyValidation), "ValidateEpochTime")]
        public int? Time
        {
            get; set;
        }
    }

    
}
