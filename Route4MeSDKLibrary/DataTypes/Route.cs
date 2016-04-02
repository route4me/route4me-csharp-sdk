using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{


  [DataContract]
  public sealed class DataObjectRoute : DataObject
  {
    [DataMember(Name = "route_id")]
    public string RouteID { get; set; }

    [DataMember(Name = "member_id")]
    public string MemberId { get; set; }

    [DataMember(Name = "member_email")]
    public string MemberEmail { get; set; }

    [DataMember(Name = "vehicle_alias")]
    public string VehicleAlias { get; set; }

    [DataMember(Name = "driver_alias")]
    public string DriverAlias { get; set; }

    [DataMember(Name = "route_cost")]
    public double? RouteCost { get; set; }

    [DataMember(Name = "route_revenue")]
    public double? RouteRevenue { get; set; }

    [DataMember(Name = "net_revenue_per_distance_unit")]
    public double? NetRevenuePerDistanceUnit { get; set; }

    [DataMember(Name = "created_timestamp")]
    public int? CreatedTimestamp { get; set; }

    [DataMember(Name = "mpg")]
    public string mpg { get; set; }

    [DataMember(Name = "trip_distance")]
    public double? TripDistance { get; set; }

    [DataMember(Name = "gas_price")]
    public double? GasPrice { get; set; }

    [DataMember(Name = "route_duration_sec")]
    public int? RouteDurationSec { get; set; }
  }
}
