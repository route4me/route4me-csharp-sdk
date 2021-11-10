using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The structure for the movement direction
    /// </summary>
    [DataContract]
    public sealed class Direction
    {
        /// <summary>
        ///     Starting location of a direction. See <see cref="DirectionLocation" />
        /// </summary>
        [DataMember(Name = "location")]
        public DirectionLocation Location { get; set; }

        /// <summary>
        ///     The diection steps. See <see cref="DirectionStep" />
        /// </summary>
        [DataMember(Name = "steps")]
        public DirectionStep[] Steps { get; set; }
    }
}