using Route4MeSDK.DataTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
  /// <summary>
  /// Avoidance zone querry
  /// </summary>
  public sealed class AvoidanceZoneQuerry : GenericParameters
  {
    /// <summary>
    /// Device Id
    /// </summary>
    [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
    public string DeviceID { get; set; }

    /// <summary>
    /// Territory Id
    /// </summary>
    [HttpQueryMemberAttribute(Name = "territory_id", EmitDefaultValue = false)]
    public string TerritoryId { get; set; }
  }
}
