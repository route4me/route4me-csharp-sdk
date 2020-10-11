using System.Runtime.Serialization;

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
        /// Distance unit
        ///</summary>
        [DataMember(Name = "distance_unit")]
        public string DistanceUnit { get; set; }

        ///<summary>
        /// Maneuver Type. Available values:
        /// <para>Head,Go Straight,Turn Left,Turn Right,Turn Slight Left,</para>
        /// <para>Turn Slight Right,Turn Sharp Left,Turn Sharp Right,</para>
        /// <para>Roundabout Left,Roundabout Right,Uturn Left,Uturn Right,</para>
        /// <para>Ramp Left,Ramp Right,Fork Left,Fork Right,Keep Left,</para>
        /// <para>Keep Right,Ferry,Ferry Train,Merge,Reached Your Destination</para>
        ///</summary>
        [DataMember(Name = "maneuverType")]
        public string ManeuverType { get; set; }

        ///<summary>
        /// Compass Direction. Available values:
        /// <para> N, S, W, E, NW, NE, SW, SE</para>
        ///</summary>
        [DataMember(Name = "compass_direction")]
        public string CompassDirection { get; set; }

        ///<summary>
        /// UDU Distance (UDU: User Distance Unit).
        ///</summary>
        [DataMember(Name = "udu_distance")]
        public double? UduDistance { get; set; }

        ///<summary>
        /// Direction step duration(seconds)
        ///</summary>
        [DataMember(Name = "duration_sec")]
        public int DurationSec { get; set; }

        ///<summary>
        /// Maneuver Point. See <see cref="DirectionPathPoint"/>
        ///</summary>
        [DataMember(Name = "maneuverPoint")]
        public DirectionPathPoint ManeuverPoint { get; set; }
    }
}
