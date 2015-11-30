using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class Activity
  {
    [DataMember(Name = "activity_id")]
    public string ActivityId { get; set; }

    [DataMember(Name = "activity_type")]
    public string ActivityType { get; set; }

    [DataMember(Name = "activity_timestamp", EmitDefaultValue = false)]
    public uint? ActivityTimestamp { get; set; }

    [DataMember(Name = "activity_message")]
    public string ActivityMessage { get; set; }
  }
}
