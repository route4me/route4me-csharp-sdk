using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Vehicle search parameters.
    /// </summary>
    [DataContract]
    public sealed class VehicleSearchParameters : GenericParameters
    {
        /// <summary>
        ///     An array of the vehicle IDs.
        /// </summary>
        [DataMember(Name = "vehicle_ids", EmitDefaultValue = false)]
        public string[] VehicleIDs { get; set; }

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