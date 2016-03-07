using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Avoidance zone parameters
  /// </summary>
  [DataContract]
  public sealed class AvoidanceZoneParameters : GenericParameters
  {
    /// <summary>
    /// Device Id
    /// </summary>
    [IgnoreDataMember] // Don't serialize as JSON
    [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
    public string DeviceID { get; set; }

    /// <summary>
    /// Territory Id
    /// </summary>
    [DataMember(Name = "territory_id")]
    public string TerritoryId { get; set; }

    /// <summary>
    /// Territory name
    /// </summary>
    [DataMember(Name = "territory_name")]
    public string TerritoryName { get; set; }

    /// <summary>
    /// Territory color
    /// </summary>
    [DataMember(Name = "territory_color")]
    public string TerritoryColor { get; set; }

    /// <summary>
    /// Member Id
    /// </summary>
    [DataMember(Name = "member_id")]
    public string MemberId { get; set; }

    /// <summary>
    /// Territory parameters
    /// </summary>
    [DataMember(Name = "territory")]
    public Territory Territory { get; set; }
  }
}
