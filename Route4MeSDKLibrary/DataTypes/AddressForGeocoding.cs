using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public class AddressForGeocoding : GenericParameters
    {
        [DataMember(Name = "rows", EmitDefaultValue = false)]
        public AddressField[] Rows { get; set; }
    }

    [DataContract]
    public class AddressField
    {
        [DataMember(Name = "ADDRESS", EmitDefaultValue = false)]
        public string Address { get; set; }

        [DataMember(Name = "CITY", EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(Name = "STATE", EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(Name = "ZIP", EmitDefaultValue = false)]
        public string Zip { get; set; }
    }
}
