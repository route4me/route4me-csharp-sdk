using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Geographic point structure
    /// </summary>
    [DataContract]
    public sealed class GeoPoint
    {
        /// <summary>
        ///     Latitude
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     Longitude
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }
    }
}