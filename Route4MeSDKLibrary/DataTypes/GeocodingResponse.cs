using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class GeocodingResponse
    {
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string address { get; set; }

        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double lat { get; set; }

        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double lng { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string type { get; set; }

        [DataMember(Name = "confidence", EmitDefaultValue = false)]
        public string confidence { get; set; }

        [DataMember(Name = "original", EmitDefaultValue = false)]
        public string original { get; set; }
    }
}
