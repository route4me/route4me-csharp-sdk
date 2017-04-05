using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class AddressBookContactsResponse : GenericParameters
    {
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public AddressBookContact[] results { get; set; }

        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? total { get; set; }
    }
}