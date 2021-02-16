using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// Parameters for execution of a vehicle order.
    /// </summary>
    [DataContract]
    public sealed class VehicleOrderParameters : GenericParameters
    {
        /// <summary>
        /// The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        /// Latitude of a vehicle position.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude of a vehicle position.
        /// <remarks><para>Query parameter.</para></remarks>
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Longitude { get; set; }
    }
}
