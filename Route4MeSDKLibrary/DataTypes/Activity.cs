using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public sealed class Activity : GenericParameters
  {
    /// <summary>
    /// Activity Id
    /// </summary>
    [DataMember(Name = "activity_id", EmitDefaultValue = false)]
    public string ActivityId { get; set; }

    /// <summary>
    /// Activity type
    /// </summary>
    [DataMember(Name = "activity_type", EmitDefaultValue = false)]
    public string ActivityType { get; set; }

    /// <summary>
    /// Activity timestamp
    /// </summary>
    [DataMember(Name = "activity_timestamp", EmitDefaultValue = false)]
    public uint? ActivityTimestamp { get; set; }

    /// <summary>
    /// Activity message
    /// </summary>
    [DataMember(Name = "activity_message", EmitDefaultValue = false)]
    public string ActivityMessage { get; set; }

    /// <summary>
    /// Member Id
    /// </summary>
    [DataMember(Name = "member_id", EmitDefaultValue = false)]
    public string MemberId { get; set; }

    /// <summary>
    /// Route Id
    /// </summary>
    [DataMember(Name = "route_id", EmitDefaultValue = false)]
    public string RouteId { get; set; }

    /// <summary>
    /// Destination Id
    /// </summary>
    [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
    public string RouteDestinationId { get; set; }
  }
}
