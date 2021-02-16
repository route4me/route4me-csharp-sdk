using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Subcalss of the Address class. See <see cref="Address.Geocodings"/>
    /// </summary>
    [DataContract]
    public sealed class Geocoding
    {
        /// <summary>
        /// A unique identifier for the geocoding
        /// </summary>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }

        /// <summary>
        /// Specific description of the geocoding result
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Boundary box
        /// </summary>
        [DataMember(Name = "bbox", EmitDefaultValue = false)]
        public double[] Bbox { get; set; }

        /// <summary>
        /// The latitude of the geocoded address
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of the geocoded address
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double Longtude { get; set; }

        /// <summary>
        /// Confidance level in the address geocoding:
        /// <para>high, medium, low</para>
        /// </summary>
        [DataMember(Name = "confidence", EmitDefaultValue = false)]
        public string Confidence { get; set; }

        /// <summary>
        /// The postal code of the geocoded address
        /// </summary>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country region
        /// </summary>
        [DataMember(Name = "countryRegion", EmitDefaultValue = false)]
        public string CountryRegion { get; set; }

        /// <summary>
        /// The address curbside coordinates
        /// </summary>
        [DataMember(Name = "curbside_coordinates", EmitDefaultValue = false)]
        public GeoPoint CurbsideCoordinates { get; set; }

        /// <summary>
        /// The address without number
        /// </summary>
        [DataMember(Name = "address_without_number", EmitDefaultValue = false)]
        public string AaddressWithoutNumber { get; set; }

        /// <summary>
        /// The place ID
        /// </summary>
        [DataMember(Name = "place_id", EmitDefaultValue = false)]
        public string PlaceID { get; set; }
    }
}
