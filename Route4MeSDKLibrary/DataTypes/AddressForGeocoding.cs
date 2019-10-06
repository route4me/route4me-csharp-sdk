using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// The class for the addresses to be geocoded.
    /// TODO: not used yet. Check if planned to use, otherwise - remove.
    /// </summary>
    [DataContract]
    public class AddressForGeocoding : GenericParameters
    {
        [DataMember(Name = "rows", EmitDefaultValue = false)]
        public AddressField[] Rows { get; set; }
    }

    /// <summary>
    /// The subclass of the AddressForGeocoding class
    /// </summary>
    [DataContract]
    public class AddressField
    {
        /// <summary>
        /// The geographic address
        /// </summary>
        [DataMember(Name = "ADDRESS", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// A city the address belongs
        /// </summary>
        [DataMember(Name = "CITY", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// A state the address belongs
        /// </summary>
        [DataMember(Name = "STATE", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// The address zipcode
        /// </summary>
        [DataMember(Name = "ZIP", EmitDefaultValue = false)]
        public string Zip { get; set; }
    }
}
