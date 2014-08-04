using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Tracking history data-structure
  /// See https://www.assembla.com/spaces/route4me_api/wiki/Route_v4
  /// </summary>
  [DataContract]
  public sealed class TrackingHistory
  {
    [DataMember(Name = "s")]
    public double? Speed {get; set; }

    [DataMember(Name = "lt")]
    public double? Latitude { get; set; }

    [DataMember(Name = "lg")]
    public double? Longitude { get; set; }

    [DataMember(Name = "d")]
    public string D {get; set; }

    [DataMember(Name = "ts")]
    public string TimeStamp {get; set; }

    [DataMember(Name = "ts_friendly")]
    public string TimeStampFriendly { get; set; }
  }
}
