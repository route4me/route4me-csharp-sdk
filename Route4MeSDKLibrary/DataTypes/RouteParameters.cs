using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Route parameters data structure
  /// See https://www.assembla.com/wiki/show/route4me_api/Route_V4#parameters
  /// </summary>
  [DataContract]
  public sealed class RouteParameters
  {
    [DataMember(Name = "is_upload", EmitDefaultValue = false)]
    public string IsUpload { get; set; }

    [DataMember(Name = "rt", EmitDefaultValue = false)]
    public bool? RT { get; set; }

    [DataMember(Name = "route_name", EmitDefaultValue = false)]
    public string RouteName { get; set; }

    [DataMember(Name = "route_date", EmitDefaultValue = false)]
    public int? RouteDate { get; set; }

    [DataMember(Name = "route_time", EmitDefaultValue = false)]
    public object RouteTime { get; set; }

    [DataMember(Name = "shared_publicly", EmitDefaultValue = false)]
    public string SharedPublicly { get; set; }

    [DataMember(Name = "disable_optimization", EmitDefaultValue = false)]
    public bool? DisableOptimization { get; set; }

    [DataMember(Name = "optimize", EmitDefaultValue = false)]
    public string Optimize { get; set; }

    [DataMember(Name = "lock_last", EmitDefaultValue = false)]
    public bool? LockLast { get; set; }

    [DataMember(Name = "vehicle_capacity", EmitDefaultValue = false)]
    public string VehicleCapacity { get; set; }

    [DataMember(Name = "vehicle_max_distance_mi", EmitDefaultValue = false)]
    public string VehicleMaxDistanceMI { get; set; }

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

    [DataMember(Name = "dev_lat", EmitDefaultValue = false)]
    public double? DevLatitude { get; set; }

    [DataMember(Name = "dev_lng", EmitDefaultValue = false)]
    public double? DevLongitude { get; set; }

    [DataMember(Name = "route_max_duration", EmitDefaultValue = false)]
    public int? RouteMaxDuration { get; set; }

    [DataMember(Name = "route_email", EmitDefaultValue = false)]
    public string RouteEmail { get; set; }

    [DataMember(Name = "route_type", EmitDefaultValue = false)]
    public string RouteType { get; set; }

    [DataMember(Name = "store_route", EmitDefaultValue = false)]
    public bool? StoreRoute { get; set; }

    [DataMember(Name = "metric", EmitDefaultValue = false)]
    public Metric Metric { get; set; }

    [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
    public AlgorithmType AlgorithmType { get; set; }

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

    [DataMember(Name = "optimization_quality", EmitDefaultValue = false)]
    public int? OptimizationQuality { get; set; }
  }
}
