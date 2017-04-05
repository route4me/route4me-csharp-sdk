using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Direction Step
  /// </summary>
  [DataContract]
  public sealed class DirectionStep
  {
    ///<summary>
    /// Name (detailed)
    ///</summary>
    [DataMember(Name = "direction")]
    public string Direction { get; set; }

    ///<summary>
    /// Name (brief)
    ///</summary>
    [DataMember(Name = "directions")]
    public string Directions { get; set; }

    ///<summary>
    /// Distance
    ///</summary>
    [DataMember(Name = "distance")]
    public double Distance { get; set; }

    ///<summary>
    /// Distance units
    ///</summary>
    [DataMember(Name = "distance_unit")]
    public string DistanceUnit { get; set; }

    ///<summary>
    /// Maneuver Type
    ///</summary>
    [DataMember(Name = "maneuverType")]
    public string ManeuverType { get; set; }

    ///<summary>
    /// Compass Direction
    ///</summary>
    [DataMember(Name = "compass_direction")]
    public string CompassDirection { get; set; }

    ///<summary>
    /// Duration (seconds)
    ///</summary>
    [DataMember(Name = "duration_sec")]
    public int DurationSec { get; set; }

    ///<summary>
    /// Maneuver Point
    ///</summary>
    [DataMember(Name = "maneuverPoint")]
    public DirectionPathPoint ManeuverPoint { get; set; }
  }
}
