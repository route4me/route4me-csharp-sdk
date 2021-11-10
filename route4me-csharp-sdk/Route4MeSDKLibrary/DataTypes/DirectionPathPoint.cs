using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Direction Path Point
    /// </summary>
    [DataContract]
    public sealed class DirectionPathPoint
    {
        /// <summary>
        ///     Latitude
        /// </summary>
        [DataMember(Name = "lat")]
        public double Lat { get; set; }

        /// <summary>
        ///     Longitude
        /// </summary>
        [DataMember(Name = "lng")]
        public double Lng { get; set; }
    }
}