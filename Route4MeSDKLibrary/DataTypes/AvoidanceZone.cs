using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Avoidance Zone
  /// </summary>
  [DataContract]
  public sealed class AvoidanceZone
  {
    ///<summary>
    /// Avoidance zone id
    ///</summary>
    [DataMember(Name = "territory_id")]
    public string TerritoryId { get; set; }

    ///<summary>
    /// Territory name
    ///</summary>
    [DataMember(Name = "territory_name")]
    public string TerritoryName { get; set; }

    ///<summary>
    /// Territory color 
    ///</summary>
    [DataMember(Name = "territory_color")]
    public string TerritoryColor { get; set; }

    ///<summary>
    /// Member Id
    ///</summary>
    [DataMember(Name = "member_id")]
    public string MemberId { get; set; }

    ///<summary>
    /// Territory parameters
    ///</summary>
    [DataMember(Name = "territory")]
    public Territory Territory { get; set; }
  }
}
