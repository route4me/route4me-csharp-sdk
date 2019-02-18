using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class AddressGeocoded
    {
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public Address geocodedAddress { get; set; }

        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int MemberId { get; set; }

        [DataMember(Name = "member_type_id", EmitDefaultValue = false)]
        public int memberTypeID { get; set; }

        [DataMember(Name = "userLat")]
        public double userLatitude { get; set; }

        [DataMember(Name = "userLng")]
        public double userLongitude { get; set; }

        [DataMember(Name = "intUserIP", EmitDefaultValue = false)]
        public uint intUserIP { get; set; }

        [DataMember(Name = "strGeocodingMethod", EmitDefaultValue = false)]
        public string strGeocodingMethod { get; set; }

        [DataMember(Name = "getCurbside", EmitDefaultValue = false)]
        public bool getCurbside { get; set; }

        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public int Priority { get; set; }
    }
}
