using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  
  [DataContract]
  public sealed class TrackingHistory
  {
    /* tracking data key names are shortened to reduce bandwidth usage (even with compression on) */
    // speed at the time of the location transaction event
    [DataMember(Name = "s")]
    public double? Speed {get; set; }

    // latitude at the time of the location transaction event
    [DataMember(Name = "lt")]
    public double? Latitude { get; set; }
    
    // longitude at the time of the location transaction event
    [DataMember(Name = "lg")]
    public double? Longitude { get; set; }

    // direction/heading at the time of the location transaction event
    [DataMember(Name = "d")]
    public string D {get; set; }

    // the original timestamp in unix timestamp format at the moment location transaction event
    [DataMember(Name = "ts")]
    public string TimeStamp {get; set; }
    
    // the original timestamp in a human readable timestamp format at the moment location transaction event
    [DataMember(Name = "ts_friendly")]
    public string TimeStampFriendly { get; set; }
  }
}
