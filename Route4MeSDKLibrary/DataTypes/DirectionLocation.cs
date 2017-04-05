using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDK.DataTypes
{
  /// <summary>
  /// Edge by edge turn-by-turn direction. 
  /// </summary>
  [DataContract]
  public sealed class DirectionLocation
  {
    ///<summary>
    /// Name
    ///</summary>
    [DataMember(Name = "name")]
    public string Name { get; set; }

    ///<summary>
    /// Segment time (seconds)
    ///</summary>
    [DataMember(Name = "time")]
    public int Time { get; set; }

    ///<summary>
    /// Segment distance
    ///</summary>
    [DataMember(Name = "segment_distance")]
    public double SegmentDistance { get; set; }

    ///<summary>
    /// Start Location
    ///</summary>
    [DataMember(Name = "start_location")]
    public string StartLocation { get; set; }

    ///<summary>
    /// End Location
    ///</summary>
    [DataMember(Name = "end_location")]
    public string EndLocation { get; set; }

    ///<summary>
    /// Directions Error
    ///</summary>
    [DataMember(Name = "directions_error")]
    public string DirectionsError { get; set; }

    ///<summary>
    /// Error Code
    ///</summary>
    [DataMember(Name = "error_code")]
    public int ErrorCode { get; set; }
  }
}
