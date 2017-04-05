using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDK.DataTypes
{
  [DataContract]
  public sealed class Direction
  {
    ///<summary>
    /// Location
    ///</summary>
    [DataMember(Name = "location")]
    public DirectionLocation Location { get; set; }

    /// <summary>
    /// Steps
    /// </summary>
    [DataMember(Name = "steps")]
    public DirectionStep[] Steps { get; set; }
  }
}
