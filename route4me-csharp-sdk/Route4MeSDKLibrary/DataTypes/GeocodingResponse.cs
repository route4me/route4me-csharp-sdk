using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Response from the address geocoding process
    /// </summary>
    [DataContract]
    public sealed class GeocodingResponse
    {
        /// <summary>
        ///     Address name
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        ///     Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Lat { get; set; }

        /// <summary>
        ///     Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double Lng { get; set; }

        /// <summary>
        ///     The address geocoding type. Available values:
        ///     <para>street_address, premise, locality, political,</para>
        ///     <para>postal_code, administrative_area_level_2, </para>
        ///     <para>political, political, administrative_area_level_1,</para>
        ///     <para>political, country, political</para>
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        ///     Confidence level in the address geocoding.
        ///     <para>Available values: high, medium, low</para>
        /// </summary>
        [DataMember(Name = "confidence", EmitDefaultValue = false)]
        public string Confidence { get; set; }

        /// <summary>
        ///     Original address string
        /// </summary>
        [DataMember(Name = "original", EmitDefaultValue = false)]
        public string Original { get; set; }
    }
}