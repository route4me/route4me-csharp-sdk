using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{


  [DataContract]
  public sealed class DataObjectRoute : DataObject
  {
    [DataMember(Name = "route_id", EmitDefaultValue = false)]
    public string RouteID { get; set; }

    [DataMember(Name = "member_id", EmitDefaultValue = false)]
    public string MemberId { get; set; }

    [DataMember(Name = "member_email", EmitDefaultValue = false)]
    public string MemberEmail { get; set; }

    [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
    public string VehicleAlias { get; set; }

    [DataMember(Name = "driver_alias", EmitDefaultValue = false)]
    public string DriverAlias { get; set; }

    [DataMember(Name = "route_cost", EmitDefaultValue = false)]
    public double? RouteCost { get; set; }

    [DataMember(Name = "route_revenue", EmitDefaultValue = false)]
    public double? RouteRevenue { get; set; }

    [DataMember(Name = "net_revenue_per_distance_unit", EmitDefaultValue = false)]
    public double? NetRevenuePerDistanceUnit { get; set; }

    [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
    public int? CreatedTimestamp { get; set; }

    [DataMember(Name = "mpg", EmitDefaultValue = false)]
    public string mpg { get; set; }

    [DataMember(Name = "trip_distance", EmitDefaultValue = false)]
    public double? TripDistance { get; set; }

    [DataMember(Name = "gas_price", EmitDefaultValue = false)]
    public double? GasPprice { get; set; }

    [DataMember(Name = "route_duration_sec", EmitDefaultValue = false)]
    public int? RouteDurationSec { get; set; }
  }
}
