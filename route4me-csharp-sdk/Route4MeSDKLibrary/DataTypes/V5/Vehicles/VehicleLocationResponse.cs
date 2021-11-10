using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the endpoint /vehicles/location
    /// </summary>
    [DataContract]
    public sealed class VehicleLocationResponse : GenericParameters
    {
        /// <summary>
        ///     An array of the vehicle locations
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleLocationItem[] Data { get; set; }
    }

    /// <summary>
    ///     Vehicle location data structure.
    /// </summary>
    public class VehicleLocationItem : GenericParameters
    {
        /// <summary>
        ///     The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     When a vehicle activity was detected.
        /// </summary>
        [DataMember(Name = "activity_timestamp", EmitDefaultValue = false)]
        public long? ActivityTimestamp { get; set; }

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
    }
}