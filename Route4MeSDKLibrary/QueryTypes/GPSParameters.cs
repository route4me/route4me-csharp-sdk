﻿
namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Helper class, for setting GPS data
  /// Used to create the suitable query string
  /// See example in Route4MeExamples.SetGPSPosition()
  /// </summary>
  public sealed class GPSParameters : GenericParameters
  {
    [HttpQueryMemberAttribute(Name = "format")]
    public string Format {get; set; }

    [HttpQueryMemberAttribute(Name = "member_id")]
    public int MemberId {get; set; }

    [HttpQueryMemberAttribute(Name = "route_id")]
    public string RouteId { get; set; }

    [HttpQueryMemberAttribute(Name = "tx_id")]
    public string TxId { get; set; }

    [HttpQueryMemberAttribute(Name = "vehicle_id")]
    public int VehicleId { get; set; }

    [HttpQueryMemberAttribute(Name = "course")]
    public int Course { get; set; }

    [HttpQueryMemberAttribute(Name = "speed")]
    public double Speed { get; set; }

    [HttpQueryMemberAttribute(Name = "lat")]
    public double Latitude { get; set; }

    [HttpQueryMemberAttribute(Name = "lng")]
    public double Longitude { get; set; }

    [HttpQueryMemberAttribute(Name = "last_position")]
    public bool last_position { get; set; }

    [HttpQueryMemberAttribute(Name = "time_period")]
    public string time_period { get; set; }

    [HttpQueryMemberAttribute(Name = "start_date")]
    public int start_date { get; set; }

    [HttpQueryMemberAttribute(Name = "end_date")]
    public int end_date { get; set; }

    [HttpQueryMemberAttribute(Name = "altitude", EmitDefaultValue = false)]
    public double Altitude { get; set; }

    [HttpQueryMemberAttribute(Name = "device_type")]
    public string DeviceType { get; set; }

    [HttpQueryMemberAttribute(Name = "device_guid")]
    public string DeviceGuid { get; set; }

    [HttpQueryMemberAttribute(Name = "device_timestamp", EmitDefaultValue = false)]
    public string DeviceTimestamp { get; set; }

    [HttpQueryMemberAttribute(Name = "app_version", EmitDefaultValue = false)]
    public string AppVersion { get; set; }


  }
}
