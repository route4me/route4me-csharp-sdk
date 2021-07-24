using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Response from the get address book contacts request
    /// </summary>
    [DataContract]
    public sealed class AddressBookContactsResponse : QueryTypes.GenericParameters
    {
        /// <summary>
        /// An array of the visible addresses
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        /// <summary>
        /// An array of the AddressBookContact type objects
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public AddressBookContact[] results { get; set; }

        /// <summary>
        /// Total number of the returned address book contacts
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? total { get; set; }
    }
}
