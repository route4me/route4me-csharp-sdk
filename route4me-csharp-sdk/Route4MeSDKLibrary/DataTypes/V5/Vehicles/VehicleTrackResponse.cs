using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the endpoint /vehicles/{id}/track
    /// </summary>
    [DataContract]
    public sealed class VehicleTrackResponse : GenericParameters
    {
        /// <summary>
        ///     An array of the vehicle locations
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleTrackItem[] Data { get; set; }
    }

    /// <summary>
    ///     Vehicle track data structure.
    /// </summary>
    public class VehicleTrackItem : GenericParameters
    {
        /// <summary>
        ///     The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     The member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        ///     Latitude of a vehicle position.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Latitude { get; set; }

        /// <summary>
        ///     Longitude of a vehicle position.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Longitude { get; set; }

        /// <summary>
        ///     The geographic altitude
        /// </summary>
        [DataMember(Name = "altitude", EmitDefaultValue = false)]
        public int? Altitude { get; set; }

        /// <summary>
        ///     Vehicle speed
        /// </summary>
        [DataMember(Name = "speed", EmitDefaultValue = false)]
        public int? Speed { get; set; }

        /// <summary>
        ///     When a vehicle activity was detected.
        /// </summary>
        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        public long? Timestamp { get; set; }
    }
}