using System.Runtime.Serialization;

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
    public int? RouteDate { get; set; }

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
    public string VehicleCapacity { get; set; }

    [DataMember(Name = "vehicle_max_distance_mi", EmitDefaultValue = false)]
    public string VehicleMaxDistanceMI { get; set; }

    //km or mi, the route4me api will convert all measurements into these units
    [DataMember(Name = "distance_unit", EmitDefaultValue = false)]
    public string DistanceUnit { get; set; }


    [DataMember(Name = "travel_mode", EmitDefaultValue = false)]
    public string TravelMode { get; set; }

    [DataMember(Name = "avoid", EmitDefaultValue = false)]
    public string Avoid { get; set; }

    [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
    public string VehicleId { get; set; }

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

    [DataMember(Name = "route_type", EmitDefaultValue = false)]
    public string RouteType { get; set; }
  
  
    //deprecated
    //all routes are stored by default at this time
    [DataMember(Name = "store_route", EmitDefaultValue = false)]
    public bool? StoreRoute { get; set; }

    [DataMember(Name = "metric", EmitDefaultValue = false)]
    public Metric Metric { get; set; }

    [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
    public AlgorithmType AlgorithmType { get; set; }

    //in order for users in your organization to have routes assigned to them, 
    //you must provide their member id within the route4me system
    //a list of member ids can be retrieved with view_users api method
    [DataMember(Name = "member_id", EmitDefaultValue = false)]
    public string MemberId { get; set; }

    [DataMember(Name = "ip", EmitDefaultValue = false)]
    public string Ip { get; set; }

    [DataMember(Name = "dm", EmitDefaultValue = false)]
    public int? DM { get; set; }

    [DataMember(Name = "dirm", EmitDefaultValue = false)]
    public int? Dirm { get; set; }

    [DataMember(Name = "parts", EmitDefaultValue = false)]
    public int? Parts { get; set; }

    [DataMember(Name = "device_id", EmitDefaultValue = false)]
    public object DeviceID { get; set; }

    [DataMember(Name = "device_type", EmitDefaultValue = false)]
    public string DeviceType { get; set; }

    [DataMember(Name = "has_trailer", EmitDefaultValue = false)]
    public bool? HasTrailer { get; set; }

    [DataMember(Name = "trailer_weight_t", EmitDefaultValue = false)]
    public double? TrailerWeightT { get; set; }

    [DataMember(Name = "limited_weight_t", EmitDefaultValue = false)]
    public double? LimitedWeightT { get; set; }

    [DataMember(Name = "weight_per_axle_t", EmitDefaultValue = false)]
    public double? WeightPerAxleT { get; set; }

    [DataMember(Name = "truck_height_meters", EmitDefaultValue = false)]
    public int? TruckHeightMeters { get; set; }

    [DataMember(Name = "truck_width_meters", EmitDefaultValue = false)]
    public int? TruckWidthMeters { get; set; }

    [DataMember(Name = "truck_length_meters", EmitDefaultValue = false)]
    public int? TruckLengthMeters { get; set; }

    [DataMember(Name = "max_tour_size", EmitDefaultValue = false)]
    public int? MaxTourSize { get; set; }

    [DataMember(Name = "min_tour_size", EmitDefaultValue = false)]
    public int? MinTourSize { get; set; }

    [DataMember(Name = "optimization_quality", EmitDefaultValue = false)]
    public int? OptimizationQuality { get; set; }
  }
}
