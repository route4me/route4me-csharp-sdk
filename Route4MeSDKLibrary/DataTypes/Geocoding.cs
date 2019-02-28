using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class Geocoding
    {
        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "bbox", EmitDefaultValue = false)]
        public double[] Bbox { get; set; }

        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Latitude { get; set; }

        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double Longtude { get; set; }

        [DataMember(Name = "confidence", EmitDefaultValue = false)]
        public string Confidence { get; set; }

        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(Name = "countryRegion", EmitDefaultValue = false)]
        public string CountryRegion { get; set; }

        [DataMember(Name = "curbside_coordinates", EmitDefaultValue = false)]
        public GeoPoint CurbsideCoordinates { get; set; }

        [DataMember(Name = "address_without_number", EmitDefaultValue = false)]
        public string AaddressWithoutNumber { get; set; }

        [DataMember(Name = "place_id", EmitDefaultValue = false)]
        public string PlaceID { get; set; }
    }
}
